using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus // Works visually, but rectangles can't be rotated. Pixel collision seems somewhat overkill, but may be the only solution
{
    public enum TypeOfLaser { Player, Enemy}
    public class Laser : Entity
    {
        public float damage;
        Timer lifeTimer;
        

        public TypeOfLaser Type { get; private set; }

    public Laser(Vector2 position, Vector2 targetPosition, Vector2 scale, TypeOfLaser type, float damage, int lifeTimer) : base(position, scale)
        {
            Type = type;
            this.damage = damage;
            //Center = position;
            Vector2 tempOrigin = new Vector2(Size.X / 2, 0);
            Origin = tempOrigin;
            Vector2 dPos = targetPosition - position;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
            this.lifeTimer = new Timer(lifeTimer);
            hitbox = new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
           
        }
       
        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            //spriteBatch.Draw(texture, hitbox, null, Color.Green, rotation, Origin, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(texture, Center, null, Color.Green, rotation, Origin, scale, SpriteEffects.None, 0.9f);
        }

        public override void HandleCollision(Entity other)
        {
            IsAlive = false;
        }

        public override void Update(GameTime gameTime)
        {
            //hitbox = new Rectangle((int)Center.X, (int)Center.Y, (int)Size.X, (int)Size.Y);
            if (lifeTimer.IsDone(gameTime))
            {
                IsAlive = false;
            }
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("GradientBar");
        }
    }
}
