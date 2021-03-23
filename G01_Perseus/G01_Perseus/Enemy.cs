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
        private Vector2 position;
        private float speed;
        private Color color;
        private Texture2D texture;
        private Point size;
        private float rotation;
        private Rectangle hitBox;
        private Vector2 offset;
        public static Vector2 posOutput;


        // remove speed and reimplement maxVelocity and acceleration
        //private float maxVelocity;
        private Vector2 direction;

        public Enemy(Vector2 position, float speed, Color color, Point size, Player player) : base()
        {
            this.position = position;
            this.speed = speed;
            this.color = color;
            this.size = size;

            offset = new Vector2(size.X / 2, size.Y / 2);
            texture = Util.CreateTexture(color, size.X, size.Y);
            hitBox = new Rectangle(GetCenter.ToPoint(), size);

            Console.WriteLine("Texture Created!");
            Console.WriteLine(texture.ToString());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, size.ToVector2() / 2, SpriteEffects.None, 0f);
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

        public void Pursue()
        {
            direction = Vector2.Zero;
            Vector2 vectorResult = Player.Position - position;
            vectorResult.Normalize();
            direction = vectorResult;
        }

        public Vector2 GetCenter { get => position + offset; private set => position = value - size.ToVector2() / 2; }
    }
}
