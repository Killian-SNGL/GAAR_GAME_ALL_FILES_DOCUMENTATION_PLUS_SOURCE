using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;

namespace GAAR_Game
{
    class EasterEgg
    {

        public Texture2D easterEggTexture;
        public Vector2 easterEggPosition;
        public Vector2 easterEggOrigin;
        public Vector2 easterEggDirection;


        public Rectangle easterEggTextureRectangle;

        public float easterEggSpeed = 3.0f;
        public float easterEggAngle = 0;
        public int easterEggMovingSense;

        public bool easterEggIsVisible = true;


        public EasterEgg(Texture2D texture)
        {
            easterEggTexture = texture;
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            easterEggTextureRectangle = new Rectangle(0, 0, easterEggTexture.Width, easterEggTexture.Height);
            spriteBatch.Draw(easterEggTexture, easterEggPosition, easterEggTextureRectangle, Color.White, easterEggAngle, easterEggOrigin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
