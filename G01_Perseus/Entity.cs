using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        protected bool isAlive;

        protected Vector2 position;
        protected Vector2 size;

        public Entity()
        {
            isAlive = true;
        }

        public Vector2 Position => position;

        public virtual Vector2 Size => size;
        
        public virtual void Update(GameTime gameTime)
        {
            if(!isAlive)
            {
                Destroy();
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight);
        protected abstract void Destroy();
    }
}