using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;

namespace GAAR_Game
{
    public class Player
    {
        public Vector2 playerPosition;
        public Texture2D playerTexture;
        public Rectangle playerTextureRectangle;
        public CollisionsCircle playerColisionsCircle;
        public float playerAngle;
        public int playerHealth =  5;
        public int playerScore = 0;
        public Vector2 playerOrigin;
        public Vector2 playerDirection;
        public float playerSpeed = 8.0f;
        public bool isDead = false;

        public Player()
        {
        }

        public Player(Vector2 position)
        {
            playerPosition = position;
        }

        public void rotateLeft()
        {
            playerAngle = playerAngle - 0.06f;
            playerDirection = new Vector2((float)Math.Sin(playerAngle), (float)Math.Cos(playerAngle));
        }

        public void rotateRight()
        {
            playerAngle = playerAngle + 0.1f;
            playerDirection = new Vector2((float)Math.Sin(playerAngle), (float)Math.Cos(playerAngle));
        }

        public void move(int width, int height) //I needs the background
        {

            playerDirection = new Vector2((float)Math.Sin(playerAngle), (float)Math.Cos(playerAngle));

            if (playerPosition.X <= width)
            {
                playerPosition.X = playerPosition.X + playerSpeed * playerDirection.X;
            }
            else
            {
                playerPosition.X = 0;
            }

            if (playerPosition.X >= 0)
            {
                playerPosition.X = playerPosition.X + playerSpeed * playerDirection.X;
            }

            else
            {
                playerPosition.X = width;
            }


            if (playerPosition.Y >= 0)
            {
                playerPosition.Y = playerPosition.Y - playerSpeed * playerDirection.Y;
            }

            else
            {
                playerPosition.Y = height;
            }

            if (playerPosition.Y <= height)
            {
                playerPosition.Y = playerPosition.Y - playerSpeed * playerDirection.Y;
            }

            else
            {
                playerPosition.Y = 0;
            }
        }

        public void shoot(List<LaserStrip> laserList, Texture2D texture)
        {
            LaserStrip newLaserStrip = new LaserStrip(texture);
            newLaserStrip.laserStripAngle = playerAngle;
            newLaserStrip.laserStripSpeed = 17.0f;
            newLaserStrip.laserStripDirection = new Vector2((float)Math.Sin(newLaserStrip.laserStripAngle), (float)Math.Cos(newLaserStrip.laserStripAngle));
            newLaserStrip.laserStripPosition.X = playerPosition.X + newLaserStrip.laserStripDirection.X * newLaserStrip.laserStripSpeed;
            newLaserStrip.laserStripPosition.Y = playerPosition.Y - newLaserStrip.laserStripDirection.Y * newLaserStrip.laserStripSpeed;
            newLaserStrip.laserStripIsVisible = true;
            laserList.Add(newLaserStrip);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle playerRectangleTexture = new Rectangle(0, 0, playerTexture.Width, playerTexture.Height);
            spriteBatch.Draw(playerTexture, playerPosition, playerRectangleTexture, Color.White, playerAngle, playerOrigin, 1.0f, SpriteEffects.None, 1);
            //Texture.....position........rectangle...............Color.......angle.........origin.......scale factor..............depth
        }

    }

}
