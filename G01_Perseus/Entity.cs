using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public abstract class Entity
    {
        protected Rectangle hitbox;
        protected float rotation, layerDepth;
        protected Vector2 scale;
        protected Texture2D texture;
        public float Rotation { get; protected set; }

        //Could maybe remove source and rotation as input parameters
        //Consider changing Roattion to a property that is initiated to 0f for all Entity objects to avoid redundancy
        public Entity(Vector2 position, Vector2 scale)
        {
            Position = position;
            this.scale = scale;
            //this.source = source;
            //this.rotation = rotation;
            //this.layerDepth = layerDepth;
            IsCollidable = true;

            if (texture == null)
            {
                DefaultTexture();
            }

            Size = texture.Bounds.Size.ToVector2() * this.scale;
            hitbox = new Rectangle(Position.ToPoint(), Size.ToPoint());

            IsAlive = true;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight);

        public abstract void HandleCollision(Entity other);

        public virtual void Destroy(Event e)
        {
            if(e != null)
            {
                EventManager.Dispatch(e);
            }
            IsAlive = false;
        }

        /// <summary>
        /// if a null value was passed at instantiation, then use this texture.
        /// </summary>
        protected abstract void DefaultTexture();

        public virtual bool IsCollidable { get; protected set; }
        public virtual bool IsAlive { get; protected set; } //Maybe move this to the MovingEntity class???
        public virtual Vector2 Size { get; protected set; }
        public virtual Vector2 Position { get; protected set; }
        public virtual Vector2 Origin { get; protected set; }

        public virtual Rectangle HitBox
        {
            get => hitbox;
            protected set => hitbox = value;
        }

        public virtual Vector2 Center
        {
            get => Position + Origin;
            protected set => Position = value - Origin;
        }
    }
}