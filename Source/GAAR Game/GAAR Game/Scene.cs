using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; //This part of the framework manages keyboard inputs
using System.Collections.Generic;
using System;
using System.Timers;

namespace GAAR_Game
{
    public abstract class Scene
    {
        public virtual void Initialize()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }


    }
}
