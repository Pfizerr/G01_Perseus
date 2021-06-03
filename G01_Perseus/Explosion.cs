using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    public class Explosion : Entity
    {
        ParticleEngine particleEngine;
        private Timer timer;
        bool activateTimer;

        public Explosion(Vector2 position, Vector2 scale, int ttl) : base(position, scale)
        {
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(AssetManager.TextureAsset("smokeParticle_sprite"));
            particleEngine = new ParticleEngine(textures, position, ttl);
            activateTimer = false;
            timer = new Timer(4000);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            particleEngine.Draw(spriteBatch);
        }

        public override void HandleCollision(Entity other)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (!activateTimer)
            {
                timer.Reset(gameTime);
                activateTimer = true;
            }
            particleEngine.Update(gameTime);
            
            if (timer.IsDone(gameTime))
            {
                IsAlive = false;
                System.Diagnostics.Debug.WriteLine("BoomDed");
            }
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("player_ship");
        }
    }
}
