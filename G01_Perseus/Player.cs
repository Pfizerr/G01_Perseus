using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{

    public class Player : Ship, CollissionListener
    {
        public Player(Vector2 position, Vector2 velocity, Vector2 scale, Rectangle? source, float rotation, float layerDepth, bool isCollidable, float health, float shield) 
            : base(position, velocity, scale, source, rotation, layerDepth, isCollidable, health, shield)
        {
            EventManager.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ShieldRegeneration(gameTime);

            AdjustAngleTowardsTarget(FindMousePosition());
            HandleInput(gameTime);

            Movement(gameTime);
            equipedWeapon.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox.Location.ToVector2(), null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, scale, SpriteEffects.None, 0.9f);
        }

        public void HandleInput(GameTime gameTime)
        {
            direction = Vector2.Zero;

            direction.Y += KeyMouseReader.KeyHold(Keys.W) ? -1 : 0;
            direction.Y += KeyMouseReader.KeyHold(Keys.S) ? 1 : 0;
            direction.X += KeyMouseReader.KeyHold(Keys.A) ? -1 : 0;
            direction.X += KeyMouseReader.KeyHold(Keys.D) ? 1 : 0;

            direction = direction.LengthSquared() > 1 ? Vector2.Normalize(direction) : direction;

            if(KeyMouseReader.LeftClick())
            {
                //EntityManager.CreateBullet(this, Center, Input.MouseWorldPosition);

                equipedWeapon.Fire(Center, KeyMouseReader.MouseWorldPosition, rotation, TypeOfBullet.Player, gameTime);

                EventManager.Dispatch(new PlayerShootEvent(Position, 1337));
            }

            ChangeWeapon();
        }

        public override void HandleCollision(Entity other)
        {
            if (other is Enemy enemy)
            {
                //RecieveDamage(enemy.damage);
            }
            else if (other is Bullet bullet)
            {
                RecieveDamage(bullet.damage);
                bullet.timeToLive = 0;
            }            
        }

        /// <summary>
        /// If you press the 1 or 2 key you will change the wepon type that you're using.
        /// </summary>
        private void ChangeWeapon()
        {
            if (KeyMouseReader.KeyPressed(Keys.D1))
            {
                equipedWeapon = weapons[0];
            }

            if (KeyMouseReader.KeyPressed(Keys.D2))
            {
                equipedWeapon = weapons[1];
            }
        }

        public Vector2 FindMousePosition()
        {
            Vector2 mousePosition = new Vector2(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            return mousePosition + cameraOffset;
        }

        public void RecieveRewards(int skillPointRewards, int resourceRewards, int dustRewards)
        {
            
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("player_ship");
        }

        public void Collision(CollissionEvent e)
        {
            HandleCollision(e.OtherEntity);
        }

        public PlayerStatus Status
        {
            get => playerStatus;
            private set => playerStatus = value;
        }


    }
}