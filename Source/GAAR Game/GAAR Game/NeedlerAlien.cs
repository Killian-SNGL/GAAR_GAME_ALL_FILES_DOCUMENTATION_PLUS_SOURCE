using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;

namespace GAAR_Game
{
    public class NeedlerAlien
    {
        public Vector2 needlerAlienPosition;
        public Texture2D needlerAlienTexture;

        public Vector2 needlerAlienOrigin;
        public float needlerAlienAngle;
        public float needlerAlienSpeed;

        public Rectangle needlerAlienTextureRectangle;
        public CollisionsCircle needlerAlienColisionsCircle;


        public bool needlerAlienIsVisible;

        public NeedlerAlien(Texture2D texture)
        {
            needlerAlienTexture = texture;
            needlerAlienIsVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            needlerAlienTextureRectangle = new Rectangle(0, 0, needlerAlienTexture.Width, needlerAlienTexture.Height);
            spriteBatch.Draw(needlerAlienTexture, needlerAlienPosition, needlerAlienTextureRectangle, Color.White, needlerAlienAngle, needlerAlienOrigin, 1.0f, SpriteEffects.None, 1);
        }


    }
}
