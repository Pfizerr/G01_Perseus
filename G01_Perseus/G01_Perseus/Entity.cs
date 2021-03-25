using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        protected bool isAlive;
        public Entity()
        {
            isAlive = true;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            if(!isAlive)
            {
                Destroy();
            }
        }
        public abstract void Draw(SpriteBatch spriteBatch);
        protected abstract void Destroy();
    }
}