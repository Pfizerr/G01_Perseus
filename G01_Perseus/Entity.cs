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
        
        public Entity(Vector2 position, Vector2 scale, Texture2D texture)
        {
            Position = position;
            this.scale = scale;
            this.texture = texture;
            IsCollidable = true;

            Size = texture.Bounds.Size.ToVector2() * this.scale;
            hitbox = new Rectangle(Position.ToPoint(), Size.ToPoint());
            Origin = Size / 2;

            IsAlive = true;
        }

        //Update and draw loop
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight);

        /// <summary>
        /// Handle colission between entities such as ships  and bullets
        /// </summary>
        /// <param name="other"></param>
        public abstract void HandleCollision(Entity other);

        /// <summary>
        /// Sets the status of the entity to be removed the next update
        /// </summary>
        /// <param name="e"></param>
        public virtual void Destroy(Event e)
        {
            if(e != null)
            {
                EventManager.Dispatch(e);
            }
            IsAlive = false;
        }

        //Properites
        public float Rotation { get; protected set; }
        public virtual bool IsCollidable { get; protected set; }
        public virtual bool IsAlive { get; protected set; } 
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