using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;
using System.Timers;


namespace GAAR_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game //PRESS F12 ON GAME TO SEE THE CODE OF GAME. GAME IS THE BASE CLASS. GAME MAP IS AN DERIVED CLASS
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Scene> sceneList = new List<Scene>();
        Scene sceneCurrent;

        GamePlay gamePlay;
        EndGame endGame;
        MainMenu mainMenu;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920; // To change the width resolution
            graphics.PreferredBackBufferHeight = 1080; // To change the height resolution
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;
            mainMenu = new MainMenu(graphics, Content);
            gamePlay = new GamePlay(graphics, Content);
            endGame = new EndGame(gamePlay.player.playerScore, graphics, Content);


            sceneList.Add(mainMenu);
            sceneList.Add(gamePlay);
            sceneList.Add(endGame);

            sceneCurrent = sceneList[0];
            sceneCurrent.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            /// Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sceneCurrent.LoadContent();

            /// TODO: use this.Content to load your game content here
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            sceneCurrent.UnloadContent();
            /// TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) //For the keyboard management
                                                          //We must check the inputs at every frame
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); // If the escape key is pressed down or if the back button of the controller is pressed, than with we exit.

            sceneCurrent.Update(gameTime);

            if (mainMenu.isGameIsAboutToBeLaunched == true)
            {
                mainMenu.isGameIsAboutToBeLaunched = false;
                mainMenu.UnloadContent();
                gamePlay.isGameMusicStopped = mainMenu.gameMusicIsStopped;
                sceneCurrent = sceneList[1];
                sceneCurrent.Initialize();
                sceneCurrent.LoadContent();
            }


            if (gamePlay.player.playerHealth <= 0)
            {
                MediaPlayer.Stop();

                gamePlay.player.playerHealth = 5; /* TO NOT HAVE A INFINITE LOOP*/
                gamePlay.UnloadContent();
                sceneCurrent = sceneList[2];
                endGame.isGameMusicStopped = mainMenu.gameMusicIsStopped;
                endGame.scoreToDisplay = gamePlay.player.playerScore;
                endGame.saveTheScore();
                sceneCurrent.Initialize();
                sceneCurrent.LoadContent();
                              
            }

            if (endGame.wannaReplay == true)
            {
                endGame.wannaReplay = false;
                endGame.UnloadContent();
                sceneCurrent = sceneList[1];
                gamePlay.player.playerScore = 0;

                for (int i = 0; i < gamePlay.asteroidList.Count; i++)
                {
                    gamePlay.asteroidList.RemoveAt(i);
                    i--;
                }

                for(int i = 0; i < gamePlay.alienList.Count; i++)
                {

                    gamePlay.alienList.RemoveAt(i);
                    i--;
                }

                for (int i = 0; i < gamePlay.needlerAlienList.Count; i++)
                {
                    gamePlay.needlerAlienList.RemoveAt(i);
                    i--;
                }

                sceneCurrent.Initialize();
                sceneCurrent.LoadContent();
            }
            /// TODO: Add your update logic here
          
            base.Update(gameTime);

        }

        
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            /// TODO: Add your drawing code here

            spriteBatch.Begin(); //Start displaying graphical elements...

            sceneCurrent.Draw(spriteBatch);

            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
