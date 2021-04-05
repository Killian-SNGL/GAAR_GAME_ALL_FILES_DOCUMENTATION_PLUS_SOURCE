using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;

namespace GAAR_Game
{
    public class Alien
    {
        public Texture2D alienTexture;
        public Vector2 alienPosition;
        public Vector2 alienOrigin;

        public Rectangle alienTextureRectangle;
        public CollisionsCircle alienColisionsCircle;

        public float alienSpeed = 5.0f;
        public float alienAngle = 0;
        public int alienMovingSense;

        public bool alienIsVisible = false;


        public Alien(Texture2D texture)
        {
            alienTexture = texture; 
        }

        public void Shoot(List<NeedlerAlien> needlerList, Texture2D texture)
        {
            NeedlerAlien needlerShot = new NeedlerAlien(texture);
            needlerShot.needlerAlienAngle = 0;
            needlerShot.needlerAlienSpeed = 23.0f;
            needlerShot.needlerAlienPosition.X = alienPosition.X;
            needlerShot.needlerAlienPosition.Y = alienPosition.Y;
            needlerShot.needlerAlienIsVisible = true;
            needlerList.Add(needlerShot);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            alienTextureRectangle = new Rectangle(0, 0, alienTexture.Width, alienTexture.Height);
            spriteBatch.Draw(alienTexture, alienPosition, alienTextureRectangle, Color.White, alienAngle, alienOrigin, 1.0f, SpriteEffects.None, 1);
        }

    }
}
