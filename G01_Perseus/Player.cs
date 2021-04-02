using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace G01_Perseus
{
    public class Player : Entity
    {
        public static new Vector2 Position { get; private set; }
        private Texture2D texture;
        private Vector2 offset;
        private Color color;
        private float timeLastFrame;
        private float rotation;
        private float speed;
        private Vector2 scale;

        private Vector2 friction = new Vector2(0.99f, 0.99f); // I Level kanske?
        private Vector2 maxVelocity = new Vector2(250f, 250f);
        private Vector2 acceleration = new Vector2(4f, 4f);
        private Vector2 direction;
        private Vector2 velocity;

        public Player(Vector2 position, float speed, Color color, Vector2 scale, float health, Texture2D texture = null) : base()
        {
            if(texture == null)
            {
                texture = AssetManager.GetTexture("player_ship_texture");
            }
            else
            {
                this.texture = texture;
            }

            this.texture = texture;
            this.speed = speed;
            this.color = color;
            this.scale = scale;
            this.health = health;
            Position = position;

            size = texture.Bounds.Size.ToVector2() * scale;
            offset = size / 2;
            hitBox = new Rectangle(Position.ToPoint(), size.ToPoint());

            //texture = Util.CreateFilledRectangleTexture(color, (int)size.X, (int)size.Y);

            Console.WriteLine("Texture Created!");
            Console.WriteLine(texture.ToString());
        }

        public override Vector2 Size => this.size;

        public override void Update(GameTime gameTime)
        {
            AdjustAngleTowardsMousePosition();
            HandleInput();
            Movement(gameTime);
            Console.WriteLine(health);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitBox.Location.ToVector2(), null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, scale, SpriteEffects.None, 0.9f);
        }

        public void Movement(GameTime gameTime)
        {
            float timeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            float deltaTime = timeThisFrame - timeLastFrame;

            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;

            // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            velocity.X = velocity.X > maxVelocity.X ? maxVelocity.X : velocity.X;
            velocity.X = velocity.X < -maxVelocity.X ? -maxVelocity.X : velocity.X;
            velocity.Y = velocity.Y > maxVelocity.Y ? maxVelocity.Y : velocity.Y;
            velocity.Y = velocity.Y < -maxVelocity.Y ? -maxVelocity.Y : velocity.Y;

            Position += velocity * deltaTime;
            hitBox.Location = Center.ToPoint();

            timeLastFrame = timeThisFrame;
        }

        public void HandleInput()
        {
            direction = Vector2.Zero;
            direction.Y += Input.keyboardState.IsKeyDown(Input.Up) ? -1 : 0;
            direction.Y += Input.keyboardState.IsKeyDown(Input.Down) ? 1 : 0;
            direction.X += Input.keyboardState.IsKeyDown(Input.Left) ? -1 : 0;
            direction.X += Input.keyboardState.IsKeyDown(Input.Right) ? 1 : 0;
            direction = direction.LengthSquared() > 1 ? Vector2.Normalize(direction) : direction;

            if(Input.IsLeftMouseButtonClicked)
            {
                EntityManager.AddBullet(new Bullet(this, Center, Input.MouseWorldPosition, 20f, Color.Red, new Vector2(4, 4), 50, 10));
            }
        }

        public void AdjustAngleTowardsMousePosition()
        {
            Vector2 mousePosition = new Vector2(Input.mouseState.X, Input.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            Vector2 dPos = (Position + offset) - (mousePosition + cameraOffset);
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        public void AdjustAngleTowardsVelocity()
        {
            rotation = (float)Math.Atan(velocity.Y / velocity.X) + MathHelper.ToRadians(90);
        }

        public override void HandleCollision(Entity other)
        {
            
        }

        public void RecieveDamage(float damage)
        {
            health -= damage;
            
            if(health <= 0)
            {
                isAlive = false;
            }
        }

        public Vector2 Center 
        { 
            get => Position + offset; 
            private set => Position = (value - offset); 
        }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}