using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 

namespace GAAR_Game
{
    public class LaserStrip
    {
        public Vector2 laserStripPosition;
        public Texture2D laserStripTexture;

        public Vector2 laserStripOrigin;
        public Vector2 laserStripDirection;
        public float laserStripAngle;
        public float laserStripSpeed;

        public Rectangle laserStripTextureRectangle;
        public CollisionsCircle laserStripColisionsCircle;


        public bool laserStripIsVisible;

        public LaserStrip(Texture2D texture)
        {
            laserStripTexture = texture;
            laserStripIsVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            laserStripTextureRectangle = new Rectangle(0, 0, laserStripTexture.Width, laserStripTexture.Height);
            spriteBatch.Draw(laserStripTexture, laserStripPosition, laserStripTextureRectangle, Color.White, laserStripAngle, laserStripOrigin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
