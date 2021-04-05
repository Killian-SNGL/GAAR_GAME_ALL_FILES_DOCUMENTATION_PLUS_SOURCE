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
using System.Threading;


namespace GAAR_Game
{
    public class MainMenu : Scene
    {
        public GraphicsDeviceManager graphicsMainMenu;
        public ContentManager mainMenuContent;

        public Vector2 mainMenuBackgroundPosition;
        public Texture2D mainMenuBackgroundTexture;

        public Vector2 mainMenuLogoPosition;
        public Texture2D mainMenuLogoTexture;

        public Texture2D buttonButtonJumpInTextureDefault;
        public Texture2D buttonButtonJumpInTextureWhenMouseIsOn;
        public Texture2D actualButtonJumpInTexture;
        public Vector2 actualButtonJumpInPosition;

        public Texture2D buttonShowInfosDefault;
        public Texture2D buttonShowInfosCursorOn;
        public Texture2D actualButtonShowInfosTexture;
        public Vector2 actualButtonShowInfosPosition;

        public Texture2D buttonQuitShowInfosSceneTextureDefault;
        public Texture2D buttonQuitShowInfosSceneTextureWhenMouseIsOn;
        public Texture2D actualButtonQuitShowInfosSceneTexture;
        public Vector2 actualButtonQuitShowInfosScenePosition;

        public Texture2D buttonMusicIsOnTexture;
        public Texture2D buttonMusicIsOffTexture;
        public Texture2D actualButtonMusicTexture;
        public Vector2 actuelButtonMusicPosition;
        public bool stateButtonMusic = true;
        public bool isInformationsSceneIsVisible = false;
        public Texture2D backgroundInfosSceneTexture;
        public Vector2 backgroundInfosScenePosition;
        public SpriteFont MainMenuCreditsFont;
        public SpriteFont MenuInformationsFont;

        public Texture2D easterEggTexture0;
        public Texture2D easterEggTexture1;
        public Texture2D easterEggTexture2;
        public Texture2D easterEggTexture3;
        public Texture2D easterEggTexture4;
        public Texture2D easterEggTexture5;
        public Texture2D easterEggTexture6;
        public Texture2D easterEggTexture7;
        public Texture2D easterEggTexture8;
        public Texture2D easterEggTexture9;
        public Texture2D easterEggTexture10;
        public Texture2D easterEggTexture11;
        public Texture2D easterEggTexture12;

        public SoundEffect clickSound;

        public bool showMainMenuGraphicalElements = true;


        public double timePassedGenerationEasterEgg = 0;

        MouseState lastMouseState;

        public bool isGameIsAboutToBeLaunched = false;
        public bool gameMusicIsStopped = false;

        public Song afterHoursRun;

        List<EasterEgg> easterEgglist = new List<EasterEgg>();


        public MainMenu(GraphicsDeviceManager graphics, ContentManager content)
        {
            graphicsMainMenu = graphics;
            mainMenuContent = content;
        }


        public override void Initialize()
        {
            mainMenuBackgroundPosition = new Vector2(0, 0);
            mainMenuLogoPosition = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 - 290, graphicsMainMenu.PreferredBackBufferHeight / 2 - 135);
            backgroundInfosScenePosition = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState actualMouseState = Mouse.GetState();
            actualButtonJumpInPosition = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 - actualButtonJumpInTexture.Width / 2 - actualButtonShowInfosTexture.Width / 2, graphicsMainMenu.PreferredBackBufferHeight / 2 + 250);
            Rectangle areaJumpInButton = new Rectangle((int)actualButtonJumpInPosition.X, (int)actualButtonJumpInPosition.Y, actualButtonJumpInTexture.Width, actualButtonJumpInTexture.Height);

            actuelButtonMusicPosition = new Vector2(graphicsMainMenu.PreferredBackBufferWidth - actualButtonMusicTexture.Width - 10, 10);
            Rectangle areaMusicButton = new Rectangle((int)actuelButtonMusicPosition.X, (int)actuelButtonMusicPosition.Y, actualButtonMusicTexture.Width, actualButtonMusicTexture.Height);

            actualButtonShowInfosPosition = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 - actualButtonShowInfosTexture.Width / 2 + actualButtonJumpInTexture.Width / 2, graphicsMainMenu.PreferredBackBufferHeight / 2 + 250);
            Rectangle areaShowInfosButton = new Rectangle((int)actualButtonShowInfosPosition.X, (int)actualButtonShowInfosPosition.Y, actualButtonShowInfosTexture.Width, actualButtonShowInfosTexture.Height);

            actualButtonQuitShowInfosScenePosition = new Vector2(graphicsMainMenu.PreferredBackBufferWidth - actualButtonQuitShowInfosSceneTexture.Width, 0);
            Rectangle areaQuitLeaderBoardButton = new Rectangle((int)actualButtonQuitShowInfosScenePosition.X, (int)actualButtonQuitShowInfosScenePosition.Y, actualButtonQuitShowInfosSceneTexture.Width, actualButtonQuitShowInfosSceneTexture.Height);


            var mousePosition = new Point(actualMouseState.X, actualMouseState.Y);
            bool clickedMouseButton = actualMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;

            if (areaJumpInButton.Contains(mousePosition))
            {
                actualButtonJumpInTexture = buttonButtonJumpInTextureWhenMouseIsOn;

                if (clickedMouseButton)
                {
                    clickSound.Play();
                    isGameIsAboutToBeLaunched = true;
                    MediaPlayer.Stop();
                }
            }

            else
            {
                actualButtonJumpInTexture = buttonButtonJumpInTextureDefault;
            }

            if (areaMusicButton.Contains(mousePosition) && isInformationsSceneIsVisible == false)
            {
                if (clickedMouseButton == true)
                {
                    clickSound.Play();
                    if (actualButtonMusicTexture == buttonMusicIsOnTexture)
                    {
                        actualButtonMusicTexture = buttonMusicIsOffTexture;
                        MediaPlayer.Volume = 0.0f;
                        gameMusicIsStopped = true;
                    }
                    else
                    {
                        actualButtonMusicTexture = buttonMusicIsOnTexture;
                        MediaPlayer.Volume = 0.8f;
                        gameMusicIsStopped = false;
                    }

                }
            }

            if (areaShowInfosButton.Contains(mousePosition))
            {
                actualButtonShowInfosTexture = buttonShowInfosCursorOn;

                if (clickedMouseButton)
                {
                    clickSound.Play();
                    showMainMenuGraphicalElements = false;
                    isInformationsSceneIsVisible = true;
                    clickedMouseButton = false;
                }

            }
            else
            {
                actualButtonShowInfosTexture = buttonShowInfosDefault;
            }

            if (isInformationsSceneIsVisible == true)
            {
                actualButtonQuitShowInfosSceneTexture = buttonQuitShowInfosSceneTextureDefault;
                if (areaQuitLeaderBoardButton.Contains(mousePosition))
                {
                    actualButtonQuitShowInfosSceneTexture = buttonQuitShowInfosSceneTextureWhenMouseIsOn;

                    if (clickedMouseButton)
                    {
                        clickSound.Play();
                        isInformationsSceneIsVisible = false;
                        showMainMenuGraphicalElements = true;
                        clickedMouseButton = false;
                    }
                }
                else
                {
                    actualButtonQuitShowInfosSceneTexture = buttonQuitShowInfosSceneTextureDefault;
                }

            }

            timePassedGenerationEasterEgg = timePassedGenerationEasterEgg + gameTime.ElapsedGameTime.TotalSeconds;

            if (timePassedGenerationEasterEgg > 6) //I call that " the falling stars " or " the falling easters eggs 
                {
                    GenerateEasterEgg();
                    timePassedGenerationEasterEgg = 0;
                }

                UpdateEasterEgg();

                lastMouseState = Mouse.GetState();
        }

        public override void LoadContent()
        {
            mainMenuBackgroundTexture = mainMenuContent.Load<Texture2D>("game_menu_wallpaper");
            mainMenuLogoTexture = mainMenuContent.Load<Texture2D>("GAAR_logo");
            afterHoursRun = mainMenuContent.Load<Song>("after-hours-run");

            buttonButtonJumpInTextureDefault = mainMenuContent.Load<Texture2D>("jump_in_button_static");
            buttonButtonJumpInTextureWhenMouseIsOn = mainMenuContent.Load<Texture2D>("jump_in_button_cursor_on");
            actualButtonJumpInTexture = buttonButtonJumpInTextureDefault;

            buttonMusicIsOnTexture = mainMenuContent.Load<Texture2D>("music_on_icon");
            buttonMusicIsOffTexture = mainMenuContent.Load<Texture2D>("music_off_icon");
            actualButtonMusicTexture = buttonMusicIsOnTexture;

            buttonShowInfosDefault = mainMenuContent.Load<Texture2D>("showInfos_default");
            buttonShowInfosCursorOn = mainMenuContent.Load<Texture2D>("showInfos_cursoron");
            actualButtonShowInfosTexture = buttonShowInfosDefault;

            buttonQuitShowInfosSceneTextureDefault = mainMenuContent.Load<Texture2D>("quitLeaderboard_default");
            buttonQuitShowInfosSceneTextureWhenMouseIsOn = mainMenuContent.Load<Texture2D>("quitLeaderboard_cursoron");
            actualButtonQuitShowInfosSceneTexture = buttonQuitShowInfosSceneTextureDefault;
            backgroundInfosSceneTexture = mainMenuContent.Load<Texture2D>("backgroundInfos");
            MainMenuCreditsFont = mainMenuContent.Load<SpriteFont>("MainMenuCredits");
            MenuInformationsFont = mainMenuContent.Load<SpriteFont>("MenuInformations");

            easterEggTexture0 = mainMenuContent.Load<Texture2D>("game_menu_surprise_117");
            easterEggTexture1 = mainMenuContent.Load<Texture2D>("game_menu_surprise_banana");
            easterEggTexture2 = mainMenuContent.Load<Texture2D>("game_menu_surprise_linearsystems");
            easterEggTexture3 = mainMenuContent.Load<Texture2D>("game_menu_surprise_polytech");
            easterEggTexture4 = mainMenuContent.Load<Texture2D>("game_menu_surprise_kiki");
            easterEggTexture5 = mainMenuContent.Load<Texture2D>("game_menu_surprise_frog");
            easterEggTexture6 = mainMenuContent.Load<Texture2D>("game_menu_surprise_oris");
            easterEggTexture7 = mainMenuContent.Load<Texture2D>("game_menu_surprise_crepes");
            easterEggTexture8 = mainMenuContent.Load<Texture2D>("game_menu_surprise_ori");
            easterEggTexture9 = mainMenuContent.Load<Texture2D>("game_menu_surprise_tudal");
            easterEggTexture10 = mainMenuContent.Load<Texture2D>("game_menu_surprise_medal");
            easterEggTexture11 = mainMenuContent.Load<Texture2D>("game_menu_surprise_foot");
            easterEggTexture12 = mainMenuContent.Load<Texture2D>("game_menu_surprise_cat");

            clickSound = mainMenuContent.Load<SoundEffect>("clickSound");

            MediaPlayer.Play(afterHoursRun);
            MediaPlayer.Volume = 0.8f;

        }

        public override void UnloadContent()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (showMainMenuGraphicalElements == true)
            {
                spriteBatch.Draw(mainMenuBackgroundTexture, new Rectangle((int)mainMenuBackgroundPosition.X, (int)mainMenuBackgroundPosition.Y, mainMenuBackgroundTexture.Width, mainMenuBackgroundTexture.Height), Color.White);
                Rectangle mainMenuLogoRectangleTexture = new Rectangle(0, 0, mainMenuLogoTexture.Width, mainMenuLogoTexture.Height);
                Vector2 mainMenuLogoOrigin = new Vector2(mainMenuLogoTexture.Width / 2, mainMenuLogoTexture.Height / 2);
                spriteBatch.Draw(mainMenuLogoTexture, mainMenuLogoPosition, mainMenuLogoRectangleTexture, Color.White, 0, mainMenuLogoOrigin, 1.55f, SpriteEffects.None, 1);
                spriteBatch.Draw(actualButtonJumpInTexture, new Rectangle((int)actualButtonJumpInPosition.X, (int)actualButtonJumpInPosition.Y, actualButtonJumpInTexture.Width, actualButtonJumpInTexture.Height), Color.White);
                spriteBatch.Draw(actualButtonMusicTexture, new Rectangle((int)actuelButtonMusicPosition.X, (int)actuelButtonMusicPosition.Y, actualButtonMusicTexture.Width, actualButtonMusicTexture.Height), Color.White);
                spriteBatch.Draw(actualButtonShowInfosTexture, new Rectangle((int)actualButtonShowInfosPosition.X, (int)actualButtonShowInfosPosition.Y, actualButtonShowInfosTexture.Width, actualButtonShowInfosTexture.Height), Color.White);

                string MessageCredits = " ALL RIGHTS RESERVED TO THE ORIGINAL CREATOR OF THE GAME 'ASTEROIDS'. FOR EDUCATIONAL PURPOSE ONLY. POLYTECH ORLEANS - 2020 ";
                Vector2 sizeOfMessageCredits = MainMenuCreditsFont.MeasureString(MessageCredits);
                Vector2 positionMessageCredits = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 - sizeOfMessageCredits.X / 2, graphicsMainMenu.PreferredBackBufferHeight - 25);
                spriteBatch.DrawString(MainMenuCreditsFont, MessageCredits, positionMessageCredits, Color.Gray);

                string MessageSongPlayed = " Song Played : AFTER HOURS RUN - MITCH MURDER ";
                Vector2 sizeOfMessageSongPlayed = MenuInformationsFont.MeasureString(MessageSongPlayed);
                Vector2 positionMessageSongPlayed = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 + sizeOfMessageSongPlayed.X / 2 + 250, +27);
                spriteBatch.DrawString(MenuInformationsFont, MessageSongPlayed, positionMessageSongPlayed, Color.CadetBlue);

                string developedBy = " A (awesome) game developed by Killian SINGLA ";
                Vector2 sizeOfDevelopedBy = MenuInformationsFont.MeasureString(developedBy);
                Vector2 positionDevelopedBy = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 - sizeOfDevelopedBy.X / 2, graphicsMainMenu.PreferredBackBufferHeight / 2 + 425);
                spriteBatch.DrawString(MenuInformationsFont, developedBy, positionDevelopedBy, Color.LightSkyBlue);

                string thanksTo = " Special thanks to Remy Leconge, all of my friends from Polytech Orleans and elsewhere, and my family for their help and their support ";
                Vector2 sizeThanksTo = MenuInformationsFont.MeasureString(thanksTo);
                Vector2 positionSizeThanksTo = new Vector2(graphicsMainMenu.PreferredBackBufferWidth / 2 - sizeThanksTo.X / 2, graphicsMainMenu.PreferredBackBufferHeight / 2 + 445);
                spriteBatch.DrawString(MenuInformationsFont, thanksTo, positionSizeThanksTo, Color.WhiteSmoke);

                foreach (EasterEgg easterEgg in easterEgglist)
                {
                    easterEgg.Draw(spriteBatch);
                }

            }

            else if (isInformationsSceneIsVisible == true)
            {
                spriteBatch.Draw(backgroundInfosSceneTexture, new Rectangle((int)backgroundInfosScenePosition.X, (int)backgroundInfosScenePosition.Y, backgroundInfosSceneTexture.Width, backgroundInfosSceneTexture.Height), Color.White);
                spriteBatch.Draw(actualButtonQuitShowInfosSceneTexture, new Rectangle((int)actualButtonQuitShowInfosScenePosition.X, (int)actualButtonQuitShowInfosScenePosition.Y, actualButtonQuitShowInfosSceneTexture.Width, actualButtonQuitShowInfosSceneTexture.Height), Color.White);
            }
        }

        public void GenerateEasterEgg()
        {
            Random rand = new Random();
            int randomTexture = rand.Next(0, 39);
            int randomAngle = rand.Next(-3, 3);
            int movingSense;

            int random_value_Y1 = rand.Next(50, 100);
            int random_value_Y2 = rand.Next(0, 500);
            if (random_value_Y2 % 2 == 0)
            {
                random_value_Y1 = -random_value_Y1;
                movingSense = +1;
            }
            else
            {
                random_value_Y1 = graphicsMainMenu.PreferredBackBufferHeight + random_value_Y1;
                movingSense = -1;

            }


            if ( 0 < randomTexture && randomTexture < 3)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture0);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 290;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);

            }

            if (3 < randomTexture && randomTexture < 6)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture1);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 -300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (6 < randomTexture && randomTexture < 9)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture2);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }


            if (9 < randomTexture && randomTexture < 12)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture3);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (12 < randomTexture && randomTexture < 15)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture4);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (15 < randomTexture && randomTexture < 18)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture5);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (18 < randomTexture && randomTexture < 21)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture6);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (21 < randomTexture && randomTexture < 24)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture7);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (24 < randomTexture && randomTexture < 27)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture8);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (27 < randomTexture && randomTexture < 30)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture9);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (30 < randomTexture && randomTexture < 33)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture10);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (33 < randomTexture && randomTexture < 36)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture11);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

            if (36 < randomTexture && randomTexture < 39)
            {
                EasterEgg easterEgg = new EasterEgg(easterEggTexture12);
                easterEgg.easterEggAngle = randomAngle;
                easterEgg.easterEggPosition.X = 1920 - 300;
                easterEgg.easterEggPosition.Y = random_value_Y1;
                easterEgg.easterEggMovingSense = movingSense;
                easterEgglist.Add(easterEgg);
            }

          

        }

        public void UpdateEasterEgg()
        {
            foreach (EasterEgg easterEgg in easterEgglist)
            {
                easterEgg.easterEggAngle = easterEgg.easterEggAngle - 0.03f;
                easterEgg.easterEggDirection = new Vector2((float)Math.Sin(easterEgg.easterEggAngle), (float)Math.Cos(easterEgg.easterEggAngle));

                easterEgg.easterEggPosition.Y = easterEgg.easterEggPosition.Y + easterEgg.easterEggMovingSense * easterEgg.easterEggSpeed;

                if (easterEgg.easterEggPosition.Y >= graphicsMainMenu.PreferredBackBufferHeight + 200 || easterEgg.easterEggPosition.Y <= -200)
                {
                    Console.WriteLine("disapear");
                    easterEgg.easterEggIsVisible = false;
                }
            }

            for (int i = 0; i < easterEgglist.Count; i++)
            {
                if (easterEgglist[i].easterEggIsVisible == false)
                {
                    easterEgglist.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

     
    }
}
