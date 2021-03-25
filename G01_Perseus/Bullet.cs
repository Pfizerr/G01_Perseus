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
        private Vector2 position;
        private Vector2 targetPosition;
        private Vector2 direction;
        private float speed;
        private Color color;
        private Point size;
        private Texture2D texture;
        private Rectangle hitBox;
        private float rotation;
        private Vector2 origin;
        private float timeToLive;

        public Bullet(Vector2 position, Vector2 targetPosition, float speed, Color color, Point size, float timeToLive) : base()
        {
            this.position = position;
            this.targetPosition = targetPosition;
            this.speed = speed;
            this.color = color;
            this.size = size;
            this.timeToLive = timeToLive;

            isAlive = true;
            rotation = 0;
            direction = Vector2.Normalize(targetPosition - position);

            texture = Util.CreateTexture(color, size.X, size.Y);
            hitBox = new Rectangle(position.ToPoint(), size);
            origin = new Vector2(size.X / 2, size.Y / 2);
        }


        public override void Update(GameTime gameTime)
        {
            timeToLive -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToLive <= 0)
            {
                isAlive = false;
            }

            position += direction * speed;
            hitBox.Location = position.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }

        protected override void Destroy()
        {
            return; //to do before instance is destroyed
        }

        public bool IsAlive { get => isAlive; private set => isAlive = value; }
    }
}