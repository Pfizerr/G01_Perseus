using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace G01_Perseus
{
    public class Player
    {
        private Vector2 position;
        private float speed;
        private Color color;
        private Texture2D texture;
        private Point size;
        private float angle;
        private Vector2 center;
        private Vector2 velocity;
        private Vector2 direction;

        public Player(Vector2 position, float speed, Color color, Point size)
        {
            this.position = position;
            this.speed = speed;
            this.color = color;
            this.size = size; 

            texture = Util.CreateTexture(color, size.X, size.Y);
            center = new Vector2(position.X - size.X, position.Y - size.Y);

            Console.WriteLine("Texture Created!");
            Console.WriteLine(texture.ToString());
        }

        public void Update(GameTime gameTime)
        {
            AdjustAngleTowardsMousePosition();
            HandleInput();

            position += velocity;
            velocity = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X - size.X / 2, position.Y - size.Y / 2), null, Color.White, angle, new Vector2(texture.Width / 2f, texture.Height / 2f), Vector2.One, SpriteEffects.None, 0);
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
            velocity.Normalize();
        }

        public void AdjustAngleTowardsMousePosition()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            Vector2 direction = mousePosition - new Vector2(position.X - size.X / 2, position.Y - size.Y / 2);
            angle = (float)Math.Atan2(direction.Y, direction.X);
            position = mousePosition;
        }
    }
}

