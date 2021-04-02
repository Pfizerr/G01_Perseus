using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        protected bool isAlive;
        protected bool isCollidable;

        protected Vector2 position;
        protected Vector2 size;
        protected Rectangle hitBox;
        
        protected float health;

        public Entity()
        {
            isAlive = true;
        }

        public Vector2 Position => position;

        public virtual Vector2 Size => size;
        
        public virtual void Update(GameTime gameTime)
        {
            if(health <= 0)
            {
                isAlive = false;
            }

            if(!isAlive)
            {
                Destroy();
            }
        }

        public abstract void HandleCollision(Entity other);

        public virtual bool IsAlive
        {
            get => isAlive;
            set => isAlive = value;
        }

        public virtual Rectangle HitBox
        {
            get => hitBox;
            private set => hitBox = value;
        }

        public virtual bool IsCollidable
        {
            get => isCollidable;
            private set =>isCollidable = value;
        }

        public virtual float Health
        {
            get => health;
            private set => health = value;
        }

        public abstract void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight);
        protected abstract void Destroy();
    }
}