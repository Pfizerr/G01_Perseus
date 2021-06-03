using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus // Works visually, but rectangles can't be rotated. Pixel collision seems somewhat overkill, but may be the only solution
{
    public enum TypeOfLaser { Player, Enemy }
    public class Laser : Entity
    {
        public float damage;
        Timer lifeTimer;
        bool timerActivated;
        Vector2 direction;
        Vector2 targetPosition;


        public TypeOfLaser Type { get; private set; }

        public Laser(Vector2 position, Vector2 targetPosition, Vector2 scale, TypeOfLaser type, float damage, int lifeTimer, Texture2D texture) : base(position, scale, texture)
        {
            this.targetPosition = targetPosition;
            Type = type;
            this.damage = damage;
            Vector2 tempOrigin = new Vector2(Size.X / 2, 0);
            Origin = tempOrigin;
            Vector2 dPos = targetPosition - position;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
            direction = Vector2.Normalize(targetPosition - position);
            this.lifeTimer = new Timer(100);
            timerActivated = false;
            hitbox = new Rectangle(0, 0, 0, 0);
            IsCollidable = true;
            //FindAllReasonablePositions();

        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            //spriteBatch.Draw(texture, hitbox, null, Color.Green, rotation, Origin, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(texture, Center, null, Color.Green, rotation, Origin, scale, SpriteEffects.None, 0.1f);
        }

        public override void HandleCollision(Entity other)
        {
            IsAlive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!timerActivated)
            {
                lifeTimer.Reset(gameTime);
                timerActivated = true;
            }
            //hitbox = new Rectangle((int)Center.X, (int)Center.Y, (int)Size.X, (int)Size.Y);
            if (lifeTimer.IsDone(gameTime))
            {
                IsAlive = false;
            }
        }

        

        public List<Vector2> FindAllReasonablePositions()
        {
            List<Vector2> output = new List<Vector2>();
            for (int i = 0; i < 1000; i++)
            {
                output.Add(direction * i + Position);
                
                //Debug.WriteLine(direction * i * 100);
                
            }
            //Debug.WriteLine(targetPosition);
            return output;
        }
    }
}
