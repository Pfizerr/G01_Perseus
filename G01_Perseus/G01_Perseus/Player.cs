using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace G01_Perseus
{
    public class Player : Entity
    {
        public static Vector2 Position { get; private set; }
        private float speed;
        private Color color;
        private Texture2D texture;
        private Point size;
        private float rotation;
        private Rectangle hitBox;
        private Vector2 offset;

        
        // remove speed and reimplement maxVelocity and acceleration
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

        public override void Update(GameTime gameTime)
        {
            AdjustAngleTowardsMousePosition();
            HandleInput();

            Position += velocity;
            hitBox.Location = GetCenter.ToPoint();
            velocity = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, size.ToVector2() / 2, SpriteEffects.None, 0f);
        }

        public void HandleInput()
        {
            
            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.W))
            {
                velocity.Y = -speed;
            }

            if(state.IsKeyDown(Keys.A))
            {
                velocity.X = -speed;
            }

            if(state.IsKeyDown(Keys.S))
            {
                velocity.Y = speed;
            }

            if(state.IsKeyDown(Keys.D))
            {
                velocity.X = speed;
            }   
        }

        public void AdjustAngleTowardsMousePosition()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 dPos = (Position + offset) - mousePosition;

            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
        }

        public Vector2 GetCenter { get => Position + offset; private set => Position = value - size.ToVector2() / 2; }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}

