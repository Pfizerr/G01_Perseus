using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class ParticleEngine
    {
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        int ttl;

        public ParticleEngine(List<Texture2D> textures, Vector2 location, int ttl)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.ttl = ttl;
            particles = new List<Particle>();
            particles.Add(GenerateNewParticle());
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[Game1.random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(Game1.random.NextDouble() * 2 - 1), 1f * (float)(Game1.random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(Game1.random.NextDouble() * 2 - 1);
            double randColor = Game1.random.NextDouble();
            Color color;
            if (randColor > 0.75f)
            {
                color = Color.Red;
            }
            else if (randColor > 0.5f)
            {
                color = Color.Orange;
            }
            else if (randColor > 0.25f)
            {
                color = Color.OrangeRed;
            }
            else
            {
                color = Color.Yellow;
            }
            
            float size = (float)Game1.random.NextDouble() / 4;
            int ttl = this.ttl + Game1.random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Update(GameTime gameTime)
        {
            int total = 2;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
                if (particles[i].TTL <= 0)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
