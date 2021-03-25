using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace G01_Perseus
{
    public class Player : Entity
    {
        public static Vector2 Position { get; private set; }
        private Texture2D texture;
        private Rectangle hitBox;
        private Vector2 offset;
        private Color color;
        private Point size;
        private float timeLastFrame;
        private float rotation;
        private float speed;

        private Vector2 friction = new Vector2(0.9955f, 0.9955f);
        private Vector2 maxVelocity = new Vector2(250f, 250f);
        private Vector2 acceleration = new Vector2(4f, 4f);
        private Vector2 direction;
        
        //remove speed and reimplement maxVelocity and acceleration
        //private float maxVelocity;
        private Vector2 velocity;        

        public Player(Vector2 position, float speed, Color color, Point size) : base()
        {
            Position = position;
            this.speed = speed;
            this.color = color;
            this.size = size;

            offset = new Vector2(size.X / 2, size.Y / 2);
            texture = Util.CreateFilledRectangleTexture(color, size.X, size.Y);
            hitBox = new Rectangle(Position.ToPoint(), size);

            Console.WriteLine("Texture Created!");
            Console.WriteLine(texture.ToString());
        }

        public override Vector2 Size => this.size.ToVector2();

        //public Vector2 Position => this.position;

        public override void Update(GameTime gameTime)
        {
            float timeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            AdjustAngleTowardsMousePosition();
            HandleInput();
            Movement(timeThisFrame - timeLastFrame);
            timeLastFrame = timeThisFrame;
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, offset, SpriteEffects.None, 1.0f);
        }

        public void Movement(float deltaTime)
        {
            if (velocity.Length() < 0.5f && velocity.Length() > -0.5f)
                velocity = Vector2.Zero;

            velocity += direction * acceleration;
            velocity *= friction;
            velocity.X = velocity.X > maxVelocity.X ? maxVelocity.X : velocity.X;
            velocity.X = velocity.X < -maxVelocity.X ? -maxVelocity.X : velocity.X;
            velocity.Y = velocity.Y > maxVelocity.Y ? maxVelocity.Y : velocity.Y;
            velocity.Y = velocity.Y < -maxVelocity.Y ? -maxVelocity.Y : velocity.Y;
            Position += velocity * deltaTime;
            hitBox.Location = Center.ToPoint();
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
                EntityManager.AddBullet(new Bullet(Center, Input.MouseWorldPosition, 7f, Color.Red, new Point(20, 20), 10));
            }
        }

        public void AdjustAngleTowardsMousePosition()
        {
            Vector2 mousePosition = new Vector2(Input.mouseState.X, Input.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            Vector2 dPos = (Position + offset) - (mousePosition + cameraOffset);

            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
        }

        public void AdjustAngleTowardsVelocity() => rotation = (float)Math.Atan(velocity.Y / velocity.X);

        public Vector2 Center { get => Position + offset; private set => Position = value - size.ToVector2() / 2; }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}