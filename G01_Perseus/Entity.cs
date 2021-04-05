using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        protected SpriteEffects spriteEffects;
        protected Rectangle? source;
        protected Rectangle hitbox;

        protected bool isCollidable;
        protected bool isAlive;
        protected float health;
        protected float deltaTime;
        protected float totalTimeLastFrame;
        protected Vector2 velocity;
        protected Vector2 origin;
        protected Vector2 size;

        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 maxVelocity;
        protected Vector2 scale;
        protected Color color;
        protected float rotation;
        protected float layerDepth;


        public Entity(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable)
        {
            this.texture = texture;
            this.position = position;
            this.maxVelocity = maxVelocity;
            this.scale = scale;
            this.source = source;
            this.color = color;
            this.rotation = rotation;
            this.layerDepth = layerDepth;
            this.isCollidable = isCollidable;
            this.spriteEffects = spriteEffects;

            if(this.texture == null)
            {
                DefaultTexture();
            }

            size = this.texture.Bounds.Size.ToVector2() * this.scale;
            hitbox = new Rectangle(Position.ToPoint(), size.ToPoint()); 

            isAlive = true;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            var totalTimeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            deltaTime = totalTimeThisFrame - totalTimeLastFrame;
            totalTimeLastFrame = totalTimeThisFrame;

            if(health <= 0)
            {
                isAlive = false;
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight);

        public abstract void HandleCollision(Entity other);

        public abstract void Destroy();

        /// <summary>
        /// if a null value was passed at instantiation, then use this texture.
        /// </summary>
        protected abstract void DefaultTexture();

        public virtual bool IsAlive
        {
            get => isAlive;
            protected set => isAlive = value;
        }
        public virtual Vector2 Size
        {
            get => size;
            protected set => size = value;
        }

        public virtual Vector2 Position
        {
            get => position;
            protected set => position = value;
        }

        public virtual Rectangle HitBox
        {
            get => hitbox;
            protected set => hitbox = value;
        }

        public virtual bool IsCollidable
        {
            get => isCollidable;
            protected set =>isCollidable = value;
        }

        public virtual Vector2 Center
        {
            get => position + origin;
            protected set => position = value - origin; 
        }
    }
}