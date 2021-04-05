using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;
using System.Timers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace GAAR_Game
{
    public class GamePlay : Scene
    {
        public Vector2 backgroundPosition;
        public Texture2D backgroundTexture;
        public Texture2D laserStripTexture;
        public Texture2D asteroidTexture0;
        public Texture2D asteroidTexture1;
        public Texture2D asteroidTexture2;
        public Texture2D alienTexture;
        public Texture2D needlerAlienTexture;
        public SpriteFont HUDfont;

        public double timePassedGenerationAsteroids = 0;
        public double timePassedGenerationAliens = 0;
        public double timePassedGenerationEasterEgg = 0;
        public double timePassedNeedlerShoot = 0;

        public Player player;
       
        KeyboardState pastState;

        public List<LaserStrip> laserStriplist = new List<LaserStrip>();

        public List<Asteroid> asteroidList = new List<Asteroid>();

        public List<Alien> alienList = new List<Alien>();

        public List<NeedlerAlien> needlerAlienList = new List<NeedlerAlien>();

        public Random randomNumber = new Random();
        public bool detectionFission = false;
        public int numberOfAsteroidsToGenerateAfterFission;
        public Asteroid asteroidSplit;

        GraphicsDeviceManager graphicsGamePlay;
        ContentManager gamePlayContent;

        public SoundEffect laserSound;
        public SoundEffect alienSound;
        public SoundEffect needlerSound;
        public SoundEffect explosionSound;
        public SoundEffect alienIsDestroyedSound;
        public SoundEffect playerIsTouched;

        public Song guile;

        public bool isGameMusicStopped;





        public GamePlay(GraphicsDeviceManager graphics, ContentManager content)
        {
            graphicsGamePlay = graphics;
            gamePlayContent = content;
            player = new Player();
        }

        public override void Initialize()
        {
            backgroundPosition = new Vector2(0, 0); // 0,0 for the upper-left corner of the window
            Vector2 position = new Vector2(graphicsGamePlay.PreferredBackBufferWidth / 2, graphicsGamePlay.PreferredBackBufferHeight / 2);
            player.playerPosition = position;
            player.playerAngle = 0;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left)) //LEFT KEY : Rotation to the left
            {
                player.rotateLeft();
            }

            if (state.IsKeyDown(Keys.Right)) //RIGHT KEY : Rotation to the right
            {
                player.rotateRight();
            }

            if (state.IsKeyDown(Keys.Up)) //UP BUTTON : MOVE
            {
                player.move(graphicsGamePlay.PreferredBackBufferWidth, graphicsGamePlay.PreferredBackBufferHeight);
            }

            if (state.IsKeyDown(Keys.Space) && pastState.IsKeyUp(Keys.Space)) //SPACE BUTTON : SHOT
            {
                laserSound.Play();
                player.shoot(laserStriplist, laserStripTexture);
            }

            UpdateLaserStrip();

            timePassedGenerationAsteroids = timePassedGenerationAsteroids + gameTime.ElapsedGameTime.TotalSeconds;
            if (timePassedGenerationAsteroids > 3) //Each 3 seconds, 7 asteroids of random types spawn on the map
            {
                timePassedGenerationAsteroids = 0;
                for (int i = 0; i < 7; i++)
                {
                    GenerateRandomAsteroid();
                }
            }

            timePassedGenerationAliens = timePassedGenerationAliens + gameTime.ElapsedGameTime.TotalSeconds;

            if (timePassedGenerationAliens > 15) //Alien Incoming !!!
            {
                timePassedGenerationAliens = 0;
                GenerateAlien();
            }

            if (timePassedGenerationEasterEgg > 60) //I call that " the falling stars " or " the falling easters egss 
            {
                /* GenerateEasterEgg */
            }

            double timePassed = timePassedGenerationAliens + gameTime.ElapsedGameTime.TotalSeconds;

            if (player.playerHealth <= 0)
            {
                Thread.Sleep(2000);
                player.isDead = true;
                MediaPlayer.Stop();
                
                /* Console.WriteLine("DEAD"); */
            }

            UpdateAsteroid();
            UpdateAlien(gameTime);
            UpdateNeedlerAlien();

            pastState = Keyboard.GetState();

        }

        public override void LoadContent()
        {
            backgroundTexture = gamePlayContent.Load<Texture2D>("game_map_wallpaper"); // It's not mandatory to specify the file extension
            player.playerTexture = gamePlayContent.Load<Texture2D>("player_sprite");
            laserStripTexture = gamePlayContent.Load<Texture2D>("laser_strip");
            asteroidTexture0 = gamePlayContent.Load<Texture2D>("asteroid_little");
            asteroidTexture1 = gamePlayContent.Load<Texture2D>("asteroid_medium");
            asteroidTexture2 = gamePlayContent.Load<Texture2D>("asteroid_fat");
            alienTexture = gamePlayContent.Load<Texture2D>("alien_flying_saucer");
            needlerAlienTexture = gamePlayContent.Load<Texture2D>("needler_alien");
            HUDfont = gamePlayContent.Load<SpriteFont>("gameFont");
            laserSound = gamePlayContent.Load<SoundEffect>("laserSound");
            alienSound = gamePlayContent.Load<SoundEffect>("alienSound");
            needlerSound = gamePlayContent.Load<SoundEffect>("needlerSound");
            explosionSound = gamePlayContent.Load<SoundEffect>("explosionSound");
            alienIsDestroyedSound = gamePlayContent.Load<SoundEffect>("Killtacular");
            playerIsTouched = gamePlayContent.Load<SoundEffect>("health");
            guile = gamePlayContent.Load<Song>("GUILE'S THEME");

            if (isGameMusicStopped == false)
            {
                MediaPlayer.Play(guile);
                MediaPlayer.Volume = 0.7f;
            }
           
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            gamePlayContent.Unload();
            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //The rectangle indicates what part of the texture we want to draw.
            spriteBatch.Draw(backgroundTexture, new Rectangle((int)backgroundPosition.X, (int)backgroundPosition.Y, backgroundTexture.Width, backgroundTexture.Height), Color.White);
            //Give the size of the rectangle who will cover the texture (here the size of the image)     
           
            player.playerOrigin = new Vector2(player.playerTexture.Width / 2, player.playerTexture.Height / 2);
            player.Draw(spriteBatch);                                                                                                                                                            //Like in optics

            foreach (LaserStrip laserstrip in laserStriplist)
            {
                laserstrip.laserStripOrigin = new Vector2(laserstrip.laserStripTexture.Width / 2, laserstrip.laserStripTexture.Height / 2);
                laserstrip.Draw(spriteBatch);
            }

            foreach (Asteroid asteroid in asteroidList)
            {
                asteroid.asteroidOrigin = new Vector2(asteroid.asteroidTexture.Width / 2, asteroid.asteroidTexture.Height / 2);
                asteroid.Draw(spriteBatch);
            }

            foreach (Alien alien in alienList)
            {
                alien.alienOrigin = new Vector2(alien.alienTexture.Width / 2, alien.alienTexture.Height / 2);
                alien.Draw(spriteBatch);
            }

            foreach (NeedlerAlien needler in needlerAlienList)
            {
                needler.needlerAlienOrigin = new Vector2(needler.needlerAlienTexture.Width / 2, needler.needlerAlienTexture.Height / 2);
                needler.Draw(spriteBatch);
            }

            
            Vector2 positionHUD = new Vector2(25, 25);
            spriteBatch.DrawString(HUDfont, "SCORE : " + player.playerScore.ToString() + "\n" + "HEALTH : " + player.playerHealth.ToString(), positionHUD, Color.White);
            base.Draw(spriteBatch);
        }

        public void GenerateRandomAsteroid()
        {
            int random_value_texture = randomNumber.Next(0, 3); //RANGE TO 3 BECAUSE WITH (0,2), 2 ISN'T CHOSE AT ALL
            float random_value_angle = randomNumber.Next(-3, 3);

            Asteroid asteroid = new Asteroid(random_value_texture, asteroidTexture0, asteroidTexture1, asteroidTexture2);

            int random_value_X1 = randomNumber.Next(0, 125);
            int random_value_X2 = randomNumber.Next(0, 500);
            if (random_value_X2 % 2 == 0)
            {
                random_value_X1 = -random_value_X1;
            }
            else
            {
                random_value_X1 = graphicsGamePlay.PreferredBackBufferWidth + random_value_X1;
            }
            int random_value_Y1 = randomNumber.Next(0, 125);
            int random_value_Y2 = randomNumber.Next(0, 500);
            if (random_value_Y2 % 2 == 0)
            {
                random_value_Y1 = -random_value_Y1;
            }
            else
            {
                random_value_Y1 = graphicsGamePlay.PreferredBackBufferHeight + random_value_Y1;
            }

            if (-125 < random_value_X1 && random_value_X1 < 125 && -125 < random_value_Y1 && random_value_Y1 < 125) //If the asteroid spawn in the upper-left corner
            {
                asteroid.asteroidcoefTrajectory0 = randomNumber.Next(80, 120);
                asteroid.asteroidcoefTrajectory1 = 1;
                asteroid.asteroidMovingSense = 1;
                //Than this trajectory is different. Here it's an increasing affinity function
            }

            if (-125 < random_value_X1 && random_value_X1 < 125 && graphicsGamePlay.PreferredBackBufferHeight - 125 < random_value_Y1 && random_value_Y1 < graphicsGamePlay.PreferredBackBufferHeight + 125) //If the asteroid spawn in the bottom-left corner
            {
                asteroid.asteroidcoefTrajectory0 = randomNumber.Next(graphicsGamePlay.PreferredBackBufferHeight - 200, graphicsGamePlay.PreferredBackBufferHeight + 200);
                asteroid.asteroidcoefTrajectory1 = -1;
                asteroid.asteroidMovingSense = 1;

                //Than this trajectory is different. Here it's an decreasing affinity function
            }

            if (graphicsGamePlay.PreferredBackBufferWidth - 125 < random_value_X1 && random_value_X1 < graphicsGamePlay.PreferredBackBufferWidth + 125 && -125 < random_value_Y1 && random_value_Y1 < 125) //If the asteroid spawn in the upper-right corner
            {
                asteroid.asteroidcoefTrajectory0 = randomNumber.Next(graphicsGamePlay.PreferredBackBufferWidth - 400, graphicsGamePlay.PreferredBackBufferWidth + 400);
                asteroid.asteroidcoefTrajectory1 = -1;
                asteroid.asteroidMovingSense = -1;

                //Than this trajectory is different. Here it's an decreasing affinity function
            }

            if (graphicsGamePlay.PreferredBackBufferWidth - 125 < random_value_X1 && random_value_X1 < graphicsGamePlay.PreferredBackBufferWidth + 125 && graphicsGamePlay.PreferredBackBufferHeight - 125 < random_value_Y1 && random_value_Y1 < graphicsGamePlay.PreferredBackBufferHeight + 125) //If the asteroid spawn in the bottom-right corner
            {
                asteroid.asteroidcoefTrajectory0 = randomNumber.Next(-400, -200);
                asteroid.asteroidcoefTrajectory1 = 1;
                asteroid.asteroidMovingSense = -1;

                //Than this trajectory is different. Here it's an increasing affinity function
            }

            asteroid.asteroidAngle = random_value_angle;

            asteroid.asteroidPosition.X = random_value_X1;
            asteroid.asteroidPosition.Y = random_value_Y1;


            asteroid.asteroidDirection = new Vector2((float)Math.Cos(asteroid.asteroidAngle), (float)Math.Sin(asteroid.asteroidAngle));
            asteroidList.Add(asteroid);
        }

        public void GenerateAlien()
        {
            Alien alien = new Alien(alienTexture);
            alienSound.Play();
            Random randomPositionAlien = new Random();
            if (randomPositionAlien.Next(0, 10) % 2 == 0)
            {
                alien.alienMovingSense = -1;
                alien.alienPosition.X = graphicsGamePlay.PreferredBackBufferWidth + 10;
                alien.alienPosition.Y = randomPositionAlien.Next(-200, 200) + graphicsGamePlay.PreferredBackBufferHeight / 2;
            }
            else
            {
                alien.alienMovingSense = 1;
                alien.alienPosition.X = -10;
                alien.alienPosition.Y = randomPositionAlien.Next(-200, 200) + graphicsGamePlay.PreferredBackBufferHeight / 2;

            }

            alienList.Add(alien);
            alien.alienIsVisible = true;
        }

        public void UpdateLaserStrip()
        {
            foreach (LaserStrip laserStrip in laserStriplist)
            {

                laserStrip.laserStripColisionsCircle = new CollisionsCircle(laserStrip.laserStripPosition, laserStrip.laserStripTexture.Width);

                laserStrip.laserStripPosition.X = laserStrip.laserStripPosition.X + laserStrip.laserStripSpeed * laserStrip.laserStripDirection.X;
                laserStrip.laserStripPosition.Y = laserStrip.laserStripPosition.Y - laserStrip.laserStripSpeed * laserStrip.laserStripDirection.Y;

                if (laserStrip.laserStripPosition.X >= backgroundTexture.Width || laserStrip.laserStripPosition.X <= 0)
                {
                    laserStrip.laserStripIsVisible = false;
                }

                if (laserStrip.laserStripPosition.Y >= backgroundTexture.Height || laserStrip.laserStripPosition.Y <= 0)
                {
                    laserStrip.laserStripIsVisible = false;
                }
            }

            for (int i = 0; i < laserStriplist.Count; i++)
            {
                if (laserStriplist[i].laserStripIsVisible == false)
                {
                    laserStriplist.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        public void UpdateAsteroid()
        {
            player.playerColisionsCircle = new CollisionsCircle(player.playerPosition, player.playerTexture.Width / 2);

            foreach (Asteroid asteroid in asteroidList)
            {
                asteroid.asteroidAngle = asteroid.asteroidAngle - 0.01f;
                asteroid.asteroidColisionsCircle = new CollisionsCircle(asteroid.asteroidPosition, asteroid.asteroidTexture.Width / 2);
                asteroid.asteroidDirection = new Vector2((float)Math.Sin(asteroid.asteroidAngle), (float)Math.Cos(asteroid.asteroidAngle));

                asteroid.asteroidPosition.X = asteroid.asteroidPosition.X + asteroid.asteroidMovingSense * asteroid.asteroidSpeed;
                asteroid.asteroidPosition.Y = asteroid.asteroidcoefTrajectory1 * asteroid.asteroidPosition.X + asteroid.asteroidcoefTrajectory0;

                if (asteroid.asteroidColisionsCircle.ColliderDetector(player.playerColisionsCircle) == true)
                {
                    explosionSound.Play();
                    playerIsTouched.Play();
                    asteroid.asteroidIsVisible = false;
                    player.playerHealth = player.playerHealth - 1;
                    /* player.playerScore = player.playerScore - 1000; */
                }

                foreach (LaserStrip laserStrip in laserStriplist)
                {
                    if (asteroid.asteroidColisionsCircle.ColliderDetector(laserStrip.laserStripColisionsCircle) == true)
                    {
                        explosionSound.Play();

                        laserStrip.laserStripIsVisible = false;

                        if (asteroid.asteroidType == 0) //If the asteroid is a tiny asteroid, + 150 points, gg !
                        {
                            player.playerScore = player.playerScore + 150;
                        }

                        if (asteroid.asteroidType == 1) //If the asteroid is a medium asteroid, he'll selft destruct in 2 tiny asteroid + 50 points, gg !
                        {
                            player.playerScore = player.playerScore + 50;
                            detectionFission = true;
                            asteroidSplit = asteroid;
                            numberOfAsteroidsToGenerateAfterFission = 2;
                        }


                        if (asteroid.asteroidType == 2) //If the asteroid is a fat asteroid, he'll selft destruct in 3 tiny asteroids and + 10 points, gg !
                        {
                            player.playerScore = player.playerScore + 10; // + 10 points, gg !
                            detectionFission = true;
                            asteroidSplit = asteroid;
                            numberOfAsteroidsToGenerateAfterFission = 3;
                        }



                        asteroid.asteroidIsVisible = false;

                    }

                    if (asteroid.asteroidPosition.X >= graphicsGamePlay.PreferredBackBufferWidth + 200 || asteroid.asteroidPosition.X <= -200)
                    {
                        asteroid.asteroidIsVisible = false;
                    }

                    if (asteroid.asteroidPosition.Y >= graphicsGamePlay.PreferredBackBufferHeight + 200 || asteroid.asteroidPosition.Y <= -200)
                    {
                        asteroid.asteroidIsVisible = false;
                    }
                }

            }

            if (detectionFission == true)
            {
                asteroidSplit.fission(asteroidList, asteroidTexture0, asteroidTexture1, asteroidTexture2, numberOfAsteroidsToGenerateAfterFission);
                detectionFission = false;
            }

            for (int i = 0; i < asteroidList.Count; i++)
            {
                if (asteroidList[i].asteroidIsVisible == false)
                {
                    asteroidList.RemoveAt(i);
                    i = i - 1;
                }
            }


        }

        public void UpdateAlien(GameTime gameTime)
        {
            foreach (Alien alien in alienList)
            {
                timePassedNeedlerShoot = timePassedNeedlerShoot + gameTime.ElapsedGameTime.TotalSeconds;
                alien.alienColisionsCircle = new CollisionsCircle(alien.alienPosition, alien.alienTexture.Width / 2);
                alien.alienPosition.X = alien.alienPosition.X + alien.alienMovingSense * alien.alienSpeed;
                alien.alienPosition.Y = 200 * (float)Math.Sin(2 * Math.PI * 1 / 2000 * alien.alienPosition.X) + graphicsGamePlay.PreferredBackBufferHeight / 2;

                if (alien.alienPosition.X <= -100 || alien.alienPosition.X >= backgroundTexture.Width + 100)
                {
                    alien.alienIsVisible = false;
                }

                if (timePassedNeedlerShoot > 0.25) //Each 1/2s, the alien shoot a needler.
                {
                    timePassedNeedlerShoot = 0;
                    alien.Shoot(needlerAlienList, needlerAlienTexture);
                    needlerSound.Play();
                }

                foreach (LaserStrip laserStrip in laserStriplist)
                {
                    if (alien.alienColisionsCircle.ColliderDetector(laserStrip.laserStripColisionsCircle) == true)
                    {
                        alienIsDestroyedSound.Play();
                        alien.alienIsVisible = false;
                        player.playerScore = player.playerScore + 117;
                    }
                }

                if (alien.alienColisionsCircle.ColliderDetector(player.playerColisionsCircle) == true)
                {
                    alien.alienIsVisible = false;
                    player.playerHealth = player.playerHealth - 1;
                    playerIsTouched.Play();

                }

            }

            for (int i = 0; i < alienList.Count; i++)
            {
                if (alienList[i].alienIsVisible == false)
                {
                    alienList.RemoveAt(i);
                    i--;
                }
            }

        }

    
        public void UpdateNeedlerAlien()
        {
            foreach (NeedlerAlien needler in needlerAlienList)
            {

                needler.needlerAlienColisionsCircle = new CollisionsCircle(needler.needlerAlienPosition, needler.needlerAlienTexture.Width);

                needler.needlerAlienPosition.Y = needler.needlerAlienPosition.Y - needler.needlerAlienSpeed;

                if (needler.needlerAlienPosition.X >= backgroundTexture.Width || needler.needlerAlienPosition.X <= 0)
                {
                    needler.needlerAlienIsVisible = false;
                }

                if (needler.needlerAlienPosition.Y >= backgroundTexture.Height || needler.needlerAlienPosition.Y <= 0)
                {
                    needler.needlerAlienIsVisible = false;
                }


                if (needler.needlerAlienColisionsCircle.ColliderDetector(player.playerColisionsCircle) == true)
                {
                    needler.needlerAlienIsVisible = false;
                    player.playerHealth = player.playerHealth - 1;
                    playerIsTouched.Play();

                }

            }

            for (int i = 0; i < needlerAlienList.Count; i++)
            {
                if (needlerAlienList[i].needlerAlienIsVisible == false)
                {
                    needlerAlienList.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

   
    }
}
