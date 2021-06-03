using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Particle
    {
        public Texture2D Texture { get; set; }        // The texture that will be drawn to represent the particle
        public Vector2 Position { get; set; }        // The current position of the particle        
        public Vector2 Velocity { get; set; }        // The speed of the particle at the current instance
        public float Angle { get; set; }            // The current angle of rotation of the particle
        public float AngularVelocity { get; set; }    // The speed that the angle is changing
        public Color Color { get; set; }            // The color of the particle
        public float Size { get; set; }                // The size of the particle
        public int TTL { get; set; }
        int totalFrames = 5;
        int currentFrame = 0;
        Timer timer = new Timer(1000);


        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public void Update(GameTime gameTime)
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
            if (timer.IsDone(gameTime))
            {
                timer.Reset(gameTime);
                if (currentFrame < totalFrames)
                {
                    currentFrame++;                   
                }
                else
                {
                    currentFrame = 0;
                }
                
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * Texture.Width / totalFrames, 0, Texture.Width / totalFrames, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / totalFrames / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }

}
