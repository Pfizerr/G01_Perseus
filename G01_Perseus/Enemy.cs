using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace G01_Perseus
{
    public class Enemy : Entity
    {
        private float speed;
        private Color color;
        private Texture2D texture;
        private float rotation;
        private Rectangle hitBox;
        private Vector2 offset;
        private Point size;
        public static Vector2 posOutput;


        // remove speed and reimplement maxVelocity and acceleration
        //private float maxVelocity;
        private Vector2 direction;

        public Enemy(Vector2 position, float speed, Color color, Point size) : base()
        {
            this.position = position;
            this.speed = speed;
            this.color = color;
            this.size = size;

            offset = new Vector2(size.X / 2, size.Y / 2);
            texture = Util.CreateFilledRectangleTexture(color, (int)size.X, (int)size.Y);
            hitBox = new Rectangle((int)GetCenter.X, (int)GetCenter.Y, (int)size.X, (int)size.Y);

            Console.WriteLine("Texture Created!");
            Console.WriteLine(texture.ToString());
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, size.ToVector2() / 2, SpriteEffects.None, 1.0f);
        }

        public override void Update(GameTime gameTime)
        {
            Pursue();

            position += direction;
            posOutput = position;
            hitBox.Location = GetCenter.ToPoint();


            if ((Vector2.Distance(position, Player.Position)) < 200)
            {
                AdjustAngleTowardsTarget();
            }
            base.Update(gameTime);
        }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }

        public void AdjustAngleTowardsTarget()
        {

            Vector2 dPos = (position + offset) - Player.Position;

            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
        }

        public void Pursue() // Moves toward the player
        {
            direction = Vector2.Zero;
            Vector2 vectorResult = Player.Position - position;
            vectorResult.Normalize();
            direction = vectorResult;
        }

        public void Roam() // Moves following a set / random path
        {


        }

        public void Strafe() // Moves horizontally in relation to the facing direction
        {

        }

        public Vector2 GetCenter { get => position + offset; private set => position = value - size.ToVector2() / 2; }
    }
}
