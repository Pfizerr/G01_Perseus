using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        
        protected SpriteEffects spriteEffects;
        protected Rectangle? source;
        protected Rectangle hitbox;

        //protected bool isCollidable;
        //protected bool isAlive;
        protected float health;
        protected float deltaTime;
        protected float totalTimeLastFrame;
        protected Vector2 velocity;
        //protected Vector2 origin;
        //protected Vector2 size;

        protected Texture2D texture;
        //protected Vector2 position;
        protected Vector2 maxVelocity;
        protected Vector2 scale;
        protected Color color;
        protected float rotation;
        protected float layerDepth;


        public Entity(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable)
        {
            this.texture = texture;
            Position = position;
            this.maxVelocity = maxVelocity;
            this.scale = scale;
            this.source = source;
            this.color = color;
            this.rotation = rotation;
            this.layerDepth = layerDepth;
            IsCollidable = isCollidable;
            this.spriteEffects = spriteEffects;

            if(this.texture == null)
            {
                DefaultTexture();
            }

            Size = this.texture.Bounds.Size.ToVector2() * this.scale;
            hitbox = new Rectangle(Position.ToPoint(), Size.ToPoint()); 

            IsAlive = true;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            var totalTimeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            deltaTime = totalTimeThisFrame - totalTimeLastFrame;
            totalTimeLastFrame = totalTimeThisFrame;

            if(health <= 0)
            {
                IsAlive = false;
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight);

        public abstract void HandleCollision(Entity other);

        public abstract void Destroy();

        /// <summary>
        /// if a null value was passed at instantiation, then use this texture.
        /// </summary>
        protected abstract void DefaultTexture();

        public virtual bool IsAlive { get; protected set;}
        public virtual Vector2 Size { get; protected set;}
        public virtual Vector2 Position { get; protected set;}
        public virtual Vector2 Origin { get; protected set; }

        public virtual Rectangle HitBox //Kan vi ta bort denna??
        {
            get => hitbox;
            protected set => hitbox = value;
        }

        public virtual bool IsCollidable { get; protected set;}

        public virtual Vector2 Center
        {
            get => Position + Origin;
            protected set => Position = value - Origin; 
        }
    }
}