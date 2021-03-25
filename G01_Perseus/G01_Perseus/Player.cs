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
            texture = Util.CreateTexture(color, size.X, size.Y);
            hitBox = new Rectangle(GetCenter.ToPoint(), size);

            Console.WriteLine("Texture Created!");
            Console.WriteLine(texture.ToString());
        }

        //public Vector2 Position => this.position;

        public override void Update(GameTime gameTime)
        {
            float timeThisFrame = (float)gameTime.TotalGameTime.TotalSeconds;
            AdjustAngleTowardsMousePosition();
            HandleInput();
            Movement(timeThisFrame - timeLastFrame);
            timeLastFrame = timeThisFrame;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, size.ToVector2() / 2, SpriteEffects.None, 0f);
        }

        public void Movement(float deltaTime)
        {
            if (velocity.Length() < 0.5f && velocity.Length() > -0.5f)
                velocity = Vector2.Zero;

            velocity *= friction;
            velocity += direction * acceleration;
            velocity.X = velocity.X > maxVelocity.X ? maxVelocity.X : velocity.X;
            velocity.X = velocity.X < -maxVelocity.X ? -maxVelocity.X : velocity.X;
            velocity.Y = velocity.Y > maxVelocity.Y ? maxVelocity.Y : velocity.Y;
            velocity.Y = velocity.Y < -maxVelocity.Y ? -maxVelocity.Y : velocity.Y;

            //Console.WriteLine("Velocity: " + velocity + "      Direction: " + direction);
            Position += velocity * deltaTime;
            hitBox.Location = GetCenter.ToPoint();
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
                EntityManager.AddBullet(new Bullet(hitBox.Center.ToVector2(), Input.mouseState.Position.ToVector2(), 7f, Color.Red, new Point(4, 4), 10));
            }
        }

        public void AdjustAngleTowardsMousePosition()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            Vector2 dPos = (Position + offset) - (mousePosition + cameraOffset);

            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
        }

        public void AdjustAngleTowardsVelocity() => rotation = (float)Math.Atan(velocity.Y / velocity.X);

        public Vector2 GetCenter { get => Position + offset; private set => Position = value - size.ToVector2() / 2; }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}