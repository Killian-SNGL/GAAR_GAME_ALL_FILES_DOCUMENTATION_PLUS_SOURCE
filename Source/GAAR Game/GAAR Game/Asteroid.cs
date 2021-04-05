using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAAR_Game
{
    public class Asteroid
    {
        public int asteroidType;
        public Texture2D asteroidTexture;
        public Vector2 asteroidPosition;
        public Vector2 asteroidOrigin;
        public Vector2 asteroidDirection;

        public Rectangle asteroidTextureRectangle;
        public CollisionsCircle asteroidColisionsCircle;

        public float asteroidAngle;
        public float asteroidSpeed;
        public int asteroidcoefTrajectory0;
        public int asteroidcoefTrajectory1;
        public int asteroidMovingSense;

        public bool asteroidIsVisible;

        public Asteroid(int type, Texture2D texture0, Texture2D texture1, Texture2D texture2)
        {
            asteroidType = type;

            if (type == 0)
            {
                asteroidTexture = texture0;
                asteroidSpeed = 8.0f;
            }

            if (type == 1)
            {
                asteroidTexture = texture1;
                asteroidSpeed = 5.0f;
            }

            if (type == 2)
            {
                asteroidTexture = texture2;
                asteroidSpeed = 3.0f;
            }
            
            asteroidIsVisible = true;
        }

        public void fission(List<Asteroid> list, Texture2D asteroid0, Texture2D asteroid1, Texture2D asteroid2, int number)
        {
            Random changeTrajectory = new Random();

            for (int i = 0; i < number; i++) //3 tiny asteroids are generated if the asteroid is a fat asteroid, 2 if it's a medium one
            {
                Asteroid asteroidResulting = new Asteroid(0, asteroid0, asteroid1, asteroid2);
                asteroidResulting.asteroidPosition = asteroidPosition;
                asteroidResulting.asteroidAngle = asteroidAngle + changeTrajectory.Next(-3, 3) ;
                asteroidResulting.asteroidOrigin = asteroidOrigin;
                asteroidResulting.asteroidDirection = asteroidDirection;
                asteroidResulting.asteroidcoefTrajectory0 = asteroidcoefTrajectory0 - changeTrajectory.Next(-400, 400);
                asteroidResulting.asteroidcoefTrajectory1 = asteroidcoefTrajectory1;
                asteroidResulting.asteroidMovingSense = asteroidMovingSense;
                list.Add(asteroidResulting);
            }

            asteroidIsVisible = false;
   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle asteroidRectangleTexture = new Rectangle(0, 0, asteroidTexture.Width, asteroidTexture.Height);
            spriteBatch.Draw(asteroidTexture, asteroidPosition, asteroidRectangleTexture, Color.White, asteroidAngle, asteroidOrigin, 1.0f, SpriteEffects.None, 1);
        }


    }
}
