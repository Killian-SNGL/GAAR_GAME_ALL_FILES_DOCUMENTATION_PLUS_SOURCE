using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;
using System.IO;
using System.Timers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace GAAR_Game
{
  public class EndGame : Scene
    {
        GraphicsDeviceManager graphicsEndGame;
        ContentManager endGameContent;

        public Vector2 endGameBackgroundPosition;
        public Texture2D endGameBackgroundTexture;

        public Texture2D buttonShowResultsTextureDefault;
        public Texture2D buttonShowResultsTextureWhenMouseIsOn;
        public Texture2D actualButtonShowResultsTexture;
        public Vector2 actualButtonShowResultsPosition;


        public Texture2D buttonReplayTextureDefault;
        public Texture2D buttonReplayTextureWhenMouseIsOn;
        public Texture2D actualButtonReplayTexture;
        public Vector2 actualButtonReplayPosition;


        public SpriteFont endGameMessageFont;
        public SpriteFont endGameScoreFont;

        public string stringToAddToTheFileScoreCollector;
        DateTime Today = DateTime.Today;
        MouseState lastMouseState;
        StreamWriter writer;
        StreamReader reader;

        bool showEndGameGraphicalElements = true;
        Texture2D leaderBoardBackgroundTexture;
        string leaderBoardString;
        Vector2 leaderBoardBackgroundPosition;

        public Texture2D buttonQuitLeaderBoardTextureDefault;
        public Texture2D buttonQuitLeaderBoardTextureWhenMouseIsOn;
        public Texture2D actualButtonQuitLeaderBoardTexture;
        public Vector2 actualButtonQuitLeaderBoardPosition;

        public SoundEffect clickSound;
        public Song goodbye;

      
        public bool LeaderBoardisVisible = false;
        public bool isGameMusicStopped;

        public bool wannaReplay = false;
               
        public int scoreToDisplay;
        public EndGame(int score, GraphicsDeviceManager graphics, ContentManager content)
        {
                graphicsEndGame = graphics;
                endGameContent = content;
                scoreToDisplay = score;
        }

        public override void Initialize()
        {
            endGameBackgroundPosition = new Vector2(0, 0); // 0,0 for the upper-left corner of the window
            leaderBoardBackgroundPosition = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState actualMouseState = Mouse.GetState();

            actualButtonShowResultsPosition = new Vector2(graphicsEndGame.PreferredBackBufferWidth/2 - actualButtonShowResultsTexture.Width/2 - actualButtonReplayTexture.Width / 2, graphicsEndGame.PreferredBackBufferHeight/2 + 200);
            actualButtonReplayPosition = new Vector2(graphicsEndGame.PreferredBackBufferWidth / 2 - actualButtonReplayTexture.Width /2 + actualButtonShowResultsTexture.Width / 2, graphicsEndGame.PreferredBackBufferHeight / 2 + 200);
            actualButtonQuitLeaderBoardPosition = new Vector2(graphicsEndGame.PreferredBackBufferWidth - actualButtonQuitLeaderBoardTexture.Width,0);

            Rectangle areaShowOldScoresButton = new Rectangle((int)actualButtonShowResultsPosition.X,(int)actualButtonShowResultsPosition.Y, actualButtonShowResultsTexture.Width, actualButtonShowResultsTexture.Height);
            Rectangle areaReplayButton = new Rectangle((int)actualButtonReplayPosition.X, (int)actualButtonReplayPosition.Y, actualButtonReplayTexture.Width, actualButtonReplayTexture.Height);
            Rectangle areaQuitLeaderBoardButton = new Rectangle((int)actualButtonQuitLeaderBoardPosition.X, (int)actualButtonQuitLeaderBoardPosition.Y, actualButtonQuitLeaderBoardTexture.Width, actualButtonQuitLeaderBoardTexture.Height);

            var mousePosition = new Point(actualMouseState.X, actualMouseState.Y);

            bool clickedMouseButton = actualMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;

            if (areaShowOldScoresButton.Contains(mousePosition))
            {
                actualButtonShowResultsTexture = buttonShowResultsTextureWhenMouseIsOn;

                if (clickedMouseButton)
                {
                    clickSound.Play();
                    readTheScore();
                    clickedMouseButton = false;
                }
                
            }
            else
            {
                actualButtonShowResultsTexture = buttonShowResultsTextureDefault;
            }


            if (areaReplayButton.Contains(mousePosition))
            {
                actualButtonReplayTexture = buttonReplayTextureWhenMouseIsOn;

                if (clickedMouseButton)
                {
                    clickSound.Play();
                    wannaReplay = true;
                    clickedMouseButton = false;
                }
            }
            else
            {
                actualButtonReplayTexture = buttonReplayTextureDefault;
            }

            if (LeaderBoardisVisible == true)
            {
                actualButtonQuitLeaderBoardTexture = buttonQuitLeaderBoardTextureDefault;
                
                if (areaQuitLeaderBoardButton.Contains(mousePosition))
                {
                    actualButtonQuitLeaderBoardTexture = buttonQuitLeaderBoardTextureWhenMouseIsOn;

                    if (clickedMouseButton)
                    {
                        clickSound.Play();
                        LeaderBoardisVisible = false;
                        showEndGameGraphicalElements = true;
                        clickedMouseButton = false;
                    }
                }
                else
                {
                    actualButtonQuitLeaderBoardTexture = buttonQuitLeaderBoardTextureDefault;
                }

               
            }

            lastMouseState = Mouse.GetState();
        }

        public override void LoadContent()
        {
            endGameBackgroundTexture = endGameContent.Load<Texture2D>("endGame_wallpaper"); // It's not mandatory to specify the file extension
            endGameMessageFont = endGameContent.Load<SpriteFont>("gameFont");
            endGameScoreFont = endGameContent.Load<SpriteFont>("endGameScoreFont");
            buttonShowResultsTextureDefault = endGameContent.Load<Texture2D>("showOldScores_default");
            buttonShowResultsTextureWhenMouseIsOn = endGameContent.Load<Texture2D>("showOldScores_withcursoron");
            buttonReplayTextureDefault = endGameContent.Load<Texture2D>("replay_default");
            buttonReplayTextureWhenMouseIsOn = endGameContent.Load<Texture2D>("replay_cursoron");

            actualButtonShowResultsTexture = buttonShowResultsTextureDefault;
            actualButtonReplayTexture = buttonReplayTextureDefault;

            leaderBoardBackgroundTexture = endGameContent.Load<Texture2D>("leaderBoard_background");

            buttonQuitLeaderBoardTextureDefault = endGameContent.Load<Texture2D>("quitLeaderboard_default");
            buttonQuitLeaderBoardTextureWhenMouseIsOn = endGameContent.Load <Texture2D>("quitLeaderboard_cursoron");
            actualButtonQuitLeaderBoardTexture = buttonQuitLeaderBoardTextureDefault;

            clickSound = endGameContent.Load<SoundEffect>("clickSound");
            goodbye = endGameContent.Load<Song>("cornemuse");

            if (isGameMusicStopped == false)
            {
                MediaPlayer.Play(goodbye);
                MediaPlayer.Volume = 0.5f;
            }

        }

        public override void UnloadContent()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (showEndGameGraphicalElements == true)
            {
                spriteBatch.Draw(endGameBackgroundTexture, new Rectangle((int)endGameBackgroundPosition.X, (int)endGameBackgroundPosition.Y, endGameBackgroundTexture.Width, endGameBackgroundTexture.Height), Color.White);
                spriteBatch.Draw(actualButtonShowResultsTexture, new Rectangle((int)actualButtonShowResultsPosition.X, (int)actualButtonShowResultsPosition.Y, actualButtonShowResultsTexture.Width, actualButtonShowResultsTexture.Height), Color.White);
                spriteBatch.Draw(actualButtonReplayTexture, new Rectangle((int)actualButtonReplayPosition.X, (int)actualButtonReplayPosition.Y, actualButtonReplayTexture.Width, actualButtonReplayTexture.Height), Color.White);

                string Message1 = " YOU'RE DEAD AND THE EARTH DIED BECAUSE OF YOU TOO ! ";
                string Message2 = " Here's your score : ";
                string Message3 = " It's now like 3 million years after the disaster, life has developed again,\n and we're already in a complete mess.";
                Vector2 sizeOfMessage1 = endGameMessageFont.MeasureString(Message1);
                Vector2 sizeOfMessage2 = endGameMessageFont.MeasureString(Message2);
                Vector2 sizeOfMessage3 = endGameMessageFont.MeasureString(Message3);


                Vector2 sizeOfScoreString = endGameScoreFont.MeasureString(scoreToDisplay.ToString());

                Vector2 positionEndGameMessage1 = new Vector2(graphicsEndGame.PreferredBackBufferWidth / 2 - sizeOfMessage1.X / 2, graphicsEndGame.PreferredBackBufferHeight / 2 - 400);
                Vector2 positionEndGameMessage2 = new Vector2(graphicsEndGame.PreferredBackBufferWidth / 2 - sizeOfMessage2.X / 2, graphicsEndGame.PreferredBackBufferHeight / 2 - 360);
                Vector2 positionEndGameMessage3 = new Vector2(graphicsEndGame.PreferredBackBufferWidth / 2 - sizeOfMessage3.X / 2, graphicsEndGame.PreferredBackBufferHeight / 2 + 400);

                Vector2 positionEndGameScore = new Vector2(graphicsEndGame.PreferredBackBufferWidth / 2 - sizeOfScoreString.X / 2, graphicsEndGame.PreferredBackBufferHeight / 2 - sizeOfScoreString.Y / 2);


                spriteBatch.DrawString(endGameMessageFont, Message1, positionEndGameMessage1, Color.White);
                spriteBatch.DrawString(endGameMessageFont, Message2, positionEndGameMessage2, Color.White);
                spriteBatch.DrawString(endGameScoreFont, scoreToDisplay.ToString(), positionEndGameScore, Color.White);
                spriteBatch.DrawString(endGameMessageFont, Message3, positionEndGameMessage3, Color.Yellow);
            }

            else
            {
                spriteBatch.Draw(leaderBoardBackgroundTexture, new Rectangle((int)leaderBoardBackgroundPosition.X, (int)leaderBoardBackgroundPosition.Y, leaderBoardBackgroundTexture.Width, leaderBoardBackgroundTexture.Height), Color.White);
                spriteBatch.Draw(actualButtonQuitLeaderBoardTexture, new Rectangle((int)actualButtonQuitLeaderBoardPosition.X, (int)actualButtonQuitLeaderBoardPosition.Y, actualButtonQuitLeaderBoardTexture.Width, actualButtonQuitLeaderBoardTexture.Height), Color.White);
                
                string MessageA = " LEADERBOARD ";
                Vector2 sizeOfMessageA = endGameMessageFont.MeasureString(MessageA);
                Vector2 sizeOfleaderBoardString = endGameScoreFont.MeasureString(leaderBoardString);
                Vector2 positionLeaderBoardMessageA = new Vector2(graphicsEndGame.PreferredBackBufferWidth/2 -sizeOfMessageA.X/2, 10);
                Vector2 positionLeaderBoardString = new Vector2(graphicsEndGame.PreferredBackBufferWidth/2 - sizeOfleaderBoardString.X/20, 50);

                spriteBatch.DrawString(endGameMessageFont, MessageA, positionLeaderBoardMessageA, Color.Yellow);
                spriteBatch.DrawString(endGameMessageFont, leaderBoardString, positionLeaderBoardString, Color.White);

            }


        }

        public void saveTheScore()
        {
            stringToAddToTheFileScoreCollector ="Score of the game : "+scoreToDisplay.ToString()+" /// Date : "+Today.ToLongDateString()+"$";
            Console.WriteLine(stringToAddToTheFileScoreCollector);

            writer = new StreamWriter("SCOREFILEDONOTOPENANDMODIFY.txt", true); ; // By default, when we write a file, it just replace the file.
                                                           // That's why we put " true " to say that we APPEND a text.
            writer.WriteLine(stringToAddToTheFileScoreCollector);
            writer.Close();
        }

        public void readTheScore()
        {
            reader = new StreamReader("SCOREFILEDONOTOPENANDMODIFY.txt");
            leaderBoardString = reader.ReadToEnd();
            char[] separateur = { '$' };
            String[] boardScore = leaderBoardString.Split(separateur);
            leaderBoardString = "";
            for (int i = boardScore.Length-1; i >= 0; i--)
            {
                leaderBoardString += boardScore[i] + "\n";
            }
            reader.Close();
            showEndGameGraphicalElements = false;
            LeaderBoardisVisible = true;
        }

    }
}
