using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Player : Entity
    {
        //public static new Vector2 Position { get; private set; }
        private Vector2 acceleration;
        private Vector2 direction;
        private Vector2 friction;
        private Weapon equipedWeapon;
        private Weapon trippleShot = new WeaponTripleShot(1, 1);
        private Weapon singleShot = new WeaponSingleShot(1, 1);
        private List<Weapon> weapons;
        //Components
        private PlayerStatus playerStatus;

        public Player(Texture2D texture, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, float health) 
            : base(texture, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            this.health = health;
            Origin = Size / 2;

            playerStatus = new PlayerStatus(health, 0f);
            weapons = new List<Weapon>() { trippleShot, singleShot};
            equipedWeapon = weapons[0];
        #region TEMP
        friction = new Vector2(0.99f, 0.99f); // move to Level.cs ?
            acceleration = new Vector2(4, 4); // move to constructor ?
            #endregion 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            AdjustAngleTowardsMousePosition();
            HandleInput();
            Movement(gameTime);
            Console.WriteLine(health);
            equipedWeapon.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, hitbox.Location.ToVector2(), null, Color.White, rotation, texture.Bounds.Size.ToVector2() / 2, scale, SpriteEffects.None, 0.9f);
        }

        public void Movement(GameTime gameTime)
        {
            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            Util.Clamp(velocity, -maxVelocity, maxVelocity); // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            Position += velocity * deltaTime;
            hitbox.Location = Center.ToPoint();
        }

        public void HandleInput()
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
                equipedWeapon.Fire(Center, KeyMouseReader.MouseWorldPosition, rotation, TypeOfBullet.Player);
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

        public void AdjustAngleTowardsMousePosition()
        {
            Vector2 mousePosition = new Vector2(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            Vector2 dPos = (Position + Origin) - (mousePosition + cameraOffset);
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        public void RecieveDamage(float damage)
        {
            health -= damage;
            
            if(health <= 0)
            {
                IsAlive = false;
            }
        }

        public void RecieveRewards(int skillPointRewards, int resourceRewards, int dustRewards)
        {
            
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        protected override void DefaultTexture()
        {
            this.texture = AssetManager.TextureAsset("player_ship");
        }

        public PlayerStatus Status
        {
            get => playerStatus;
            private set => playerStatus = value;
        }
    }
}