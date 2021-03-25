﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Bullet : Entity
    {
        private Vector2 targetPosition;
        private Texture2D texture;
        private Vector2 direction;
        private float timeToLive;
        private Rectangle hitBox;
        private float rotation;
        private Vector2 origin;
        private float speed;
        private Color color;
        private Vector2 offset;

        public Bullet(Vector2 position, Vector2 targetPosition, float speed, Color color, Point size, float timeToLive) : base()
        {
            this.position = position;
            this.targetPosition = targetPosition;
            this.speed = speed;
            this.color = color;
            this.size = size.ToVector2();
            this.timeToLive = timeToLive;

            isAlive = true;
            rotation = 0;
            direction = Vector2.Normalize(targetPosition - position);

            texture = Util.CreateFilledRectangleTexture(color, size.X, size.Y);
            origin = new Vector2(size.X / 2, size.Y / 2);
            hitBox = new Rectangle(position.ToPoint(), size);
        }


        public override void Update(GameTime gameTime)
        {
            timeToLive -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToLive <= 0)
            {
                isAlive = false;
            }

            position += direction * speed;
            //hitBox.Location = position.ToPoint();
            hitBox.Location = Center.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, origin, SpriteEffects.None, 1f);
        }

        protected override void Destroy()
        {
            return; //to do before instance is destroyed
        }

        public bool IsAlive { get => isAlive; private set => isAlive = value; }
        public Vector2 Center { get => Position; private set => position = value - size / 2; }
    }
}