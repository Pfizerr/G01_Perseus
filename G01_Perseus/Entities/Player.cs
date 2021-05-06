using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Player : Entity
    {
        private Vector2 acceleration;
        private Vector2 direction;
        private Vector2 friction;
        private Weapon equippedWeapon = new WeaponTripleShot(1, 1);
        private bool isInteractingWithEnvironment;

        //Components
        private PlayerStatus status;

        public Player(Sprite sprite, Vector2 position, Vector2 maxVelocity, Vector2 scale, Rectangle? source, SpriteEffects spriteEffects, Color color, float rotation, float layerDepth, bool isCollidable, float health) 
            : base(sprite, position, maxVelocity, scale, source, spriteEffects, color, rotation, layerDepth, isCollidable)
        {
            this.health = health;
            origin = size / 2;

            status = new PlayerStatus(health, 0f);

            isInteractingWithEnvironment = false;

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
            equippedWeapon.Update(gameTime);

            //Console.WriteLine(health);

            if (status.Mission.Count > 0)
            {
                foreach (Mission mission in status.Mission)
                {
                Console.WriteLine(String.Format("ID: {0} Contractor: {1} Owner: {2}", mission.Id, mission.Contractor, mission.Owner));
                }
            }
            //Console.WriteLine();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            sprite.Draw(spriteBatch, hitbox.Location.ToVector2(), scale, origin, rotation, layerDepth);
        }

        public void Movement(GameTime gameTime)
        {
            if (velocity.Length() < 3f && velocity.Length() > -3f)
                velocity = Vector2.Zero;

            velocity = (velocity + direction * acceleration) * friction;
            Util.Clamp(velocity, -maxVelocity, maxVelocity); // clamp velocity to values between min value -maxVelocity and max value -maxVelocity
            position += velocity * deltaTime;
            hitbox.Location = Center.ToPoint();
        }

        public void HandleInput()
        {
            direction = Vector2.Zero;

            direction.Y += Input.keyboardState.IsKeyDown(Input.Up) ? -1 : 0;
            direction.Y += Input.keyboardState.IsKeyDown(Input.Down) ? 1 : 0;
            direction.X += Input.keyboardState.IsKeyDown(Input.Left) ? -1 : 0;
            direction.X += Input.keyboardState.IsKeyDown(Input.Right) ? 1 : 0;

            direction = direction.LengthSquared() > 1 ? Vector2.Normalize(direction) : direction;

            if(Input.IsLeftMouseButtonClicked)
            {
                //EntityManager.CreateBullet(this, Center, Input.MouseWorldPosition);
                equippedWeapon.Fire(Center, Input.MouseWorldPosition, rotation, this);
                EventManager.Dispatch(new PlayerShootEvent(position, 1337));
            }
        }

        public override void HandleCollision(Entity other)
        {

        }

        public void AdjustAngleTowardsMousePosition()
        {
            Vector2 mousePosition = new Vector2(Input.mouseState.X, Input.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            Vector2 dPos = (Position + origin) - (mousePosition + cameraOffset);
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - MathHelper.ToRadians(90);
        }

        public void RecieveDamage(float damage)
        {
            health -= damage;
            
            if(health <= 0)
            {
                isAlive = false;
            }
        }

        public void RecieveRewards(int skillPointRewards, int resourceRewards, int dustRewards)
        {
            
        }

        public void RecieveMissions(ICollection<Mission> missions)
        {
            foreach(Mission mission in missions)
            {
                status.Mission.Add(mission);
            }
        }

        public override void Destroy()
        {
            //Code to execute when destroyed..

            System.Console.WriteLine("{0} has been killed.", this.ToString());
            return;
        }

        protected override void DefaultTexture()
        {
            this.sprite = AssetManager.SpriteAsset("player_ship");
        }

        public PlayerStatus Status
        {
            get => status;
            private set => status = value;
        }
    }
}