﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace G01_Perseus
{
    public class Enemy : Ship, PlayerShootListener, CollissionListener
    {
        private Vector2 strafeVector = Vector2.Zero;
        private int healthBarHeight;
        private Rectangle healthPos;
        private Random random = new Random();
        public List<Bullet> bullets = new List<Bullet>();

        private EnemyBehavior behavior;

        public Enemy(Vector2 position, Vector2 maxVelocity, Vector2 scale, float health, float shield, EnemyBehavior behavior) : base(position, maxVelocity, scale, health, shield)
        {
            healthBarHeight = 10;
            healthPos = new Rectangle((int)position.X, (int)position.Y - healthBarHeight, hitbox.Width, healthBarHeight);
            EventManager.Register(this);

            this.behavior = behavior;
            behavior.enemy = this;

        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox, null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), healthPos, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            //Vector2 drawPosition = new Vector2((tileX * tileWidth) + position.X - (ix * tileWidth), (tileY * tileWidth) + position.Y - (iy * tileHeight));
            //spriteBatch.Draw(texture, drawPosition, null, Color.White, rotation, size / 2, Vector2.One, SpriteEffects.None, 0.5f);
        }

        public override void Update(GameTime gameTime)
        {
            this.behavior.Update(gameTime, rotation);

            Movement(gameTime);


            hitbox.Location = Center.ToPoint();
            SetHealthPosition();
            base.Update(gameTime);

        }

        public void FireWeapon(GameTime gameTime)
        {
            equipedWeapon.Fire(Center, EntityManager.Player.Position, rotation, TypeOfBullet.Enemy, gameTime);
            equipedWeapon.Update(gameTime);
        }

        private void SetHealthPosition()
        {
            healthPos.Location = Position.ToPoint();
            healthPos.Y -= healthBarHeight;
        }

        public override void HandleCollision(Entity other)
        {
            if (other is Player player)
            {
                //RecieveDamage(10); //Replace with player.damage when this is implemented
            }
            else if (other is Bullet bullet)
            {
                RecieveDamage(bullet.damage);
                bullet.timeToLive = 0;
            }

        }

        public override void RecieveDamage(float damage)
        {
            base.RecieveDamage(damage);
            healthPos.Width = (int)(hitbox.Width * (health / maxHealth));
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("enemy_ship");
        }

        public void PlayerFired(PlayerShootEvent e)
        {
            //System.Diagnostics.Debug.WriteLine("Player has fired at position "+e.position +" with damage: "+e.damage);
        }

        public void Collision(CollissionEvent e)
        {
            System.Diagnostics.Debug.WriteLine("Entity: " + e.Entity + " collided with: " + e.OtherEntity); //debug

            HandleCollision(e.OtherEntity);
        }
    }
}