using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        protected bool isAlive;

        protected Vector2 position;
        protected Vector2 size;
        protected Vector2 offset;
        protected Vector2 scale;
        protected Rectangle hitBox;
        protected Texture2D texture;

        public Entity()
        {
            isAlive = true;
            offset = new Vector2(size.X / 2, size.Y / 2);
            hitBox = new Rectangle(position.ToPoint() + offset.ToPoint(), size.ToPoint());
    }

        public virtual Vector2 Position => position;

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

        public virtual Vector2 Center { get => Position + offset; private set => hitBox.Location = (value - offset).ToPoint(); }
    }
}