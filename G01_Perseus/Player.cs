using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Player : Ship, CollissionListener, PlanetInteractionListener, MouseEnterPlanetListener, MouseExitPlanetListener
    {
        private bool hasFocusOnPlanet;
        private float baseMaxHealth;
        private float baseMaxShields;
        private float basePowerLevel;
        public bool uppdatePowerlevel;
        public enum Addons { Disruptor, LifeSteal, Piercing, Freeze}
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="scale"></param>
        /// <param name="health"></param>
        /// <param name="shield"></param>
        public Player(Vector2 position, Vector2 velocity, Vector2 scale, float health, float shield) : base(position, velocity, scale, health, shield)
        {
            baseMaxHealth = health;
            baseMaxShields = shield;
            basePowerLevel = 1; //This could be an input parameter for the constructor
            uppdatePowerlevel = false;
            EventManager.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            //These if statements should be moved perhaps?
            if(Shields > MaxShields)
            {
                Shields = MaxShields;
            }

            if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            base.Update(gameTime);

            ShieldRegeneration(gameTime);

            AdjustAngleTowardsTarget(FindMousePosition());
            HandleInput(gameTime);

            Movement(gameTime);
            if (uppdatePowerlevel)
            {
                UpdateWeapons();
            }
            equipedWeapon.Update(gameTime);

            if (Status.Missions.Count > 0)
            {
                foreach (Mission mission in Status.Missions)
                {
                Console.WriteLine(String.Format("ID: {0} Contractor: {1} Owner: {2}", mission.Id, mission.Contractor, mission.Owner));
                }
            }

            Status.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            spriteBatch.Draw(texture, Center, null, Color.White, rotation, texture.Bounds.Size.ToVector2() * 0.5f, scale, SpriteEffects.None, 0.9f);
            Status.Draw(spriteBatch);
            
            //spriteBatch.Draw(Util.CreateFilledRectangleTexture(Color.Blue, hitbox.Width, hitbox.Height), hitbox, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.7f); // Draw hitbox at hitbox. (debug)
        }

        /// <summary>
        /// This handles all the inputs the player can made with movement, change weapon, fire etc.
        /// </summary>
        /// <param name="gameTime"></param>
        public void HandleInput(GameTime gameTime)
        {
            direction = Vector2.Zero;

            direction.Y += KeyMouseReader.KeyHold(Keys.W) ? -1 : 0;
            direction.Y += KeyMouseReader.KeyHold(Keys.S) ? 1 : 0;
            direction.X += KeyMouseReader.KeyHold(Keys.A) ? -1 : 0;
            direction.X += KeyMouseReader.KeyHold(Keys.D) ? 1 : 0;

            direction = direction.LengthSquared() > 1 ? Vector2.Normalize(direction) : direction;

            if(KeyMouseReader.LeftClick() && !hasFocusOnPlanet)
            {
                //EntityManager.CreateBullet(this, Center, Input.MouseWorldPosition);

                equipedWeapon.Fire(Center, KeyMouseReader.MouseWorldPosition, rotation, TypeOfBullet.Player, gameTime);
                EventManager.Dispatch(new PlayerShootEvent(Position, 1337));
            }

            ChangeWeapon();
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

        /// <summary>
        /// This sets the direction of the ship in the game world through the mouse position
        /// </summary>
        /// <returns>A vector2 for the orientation and bullet direction</returns>
        public Vector2 FindMousePosition()
        {
            Vector2 mousePosition = new Vector2(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y);
            Vector3 cameraTranslation = Game1.camera.Translation.Translation;
            Vector2 cameraOffset = new Vector2(-cameraTranslation.X, -cameraTranslation.Y);
            return mousePosition + cameraOffset;
        }

        public void RecieveMission(Mission mission)
        {
            mission.SetOwner(this);
            Status.Missions.Add(mission);
        }

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("player_ship");
        }

        public PlayerStatus Status
        {
            get => playerStatus;
            private set => playerStatus = value;
        }

        public void OnMouseClick(PlanetInteractionEvent e)
        {
            List<Mission> completedMissions = new List<Mission>();
            for (int i = 0; i < Status.Missions.Count; i++)
            {
                if (Status.Missions[i].State == Mission.EState.Completed)
                {
                    completedMissions.Add(Status.Missions[i]);
                    Status.Missions.RemoveAt(i);
                    i--;
                }
            }

            e.Planet.Customer = this;
        }

        public void OnMouseEnter(MouseEnterPlanetEvent e)
        {
            hasFocusOnPlanet = true;
        }

        public void OnMouseExit(MouseExitPlanetEvent e)
        {
            hasFocusOnPlanet = false;
        }
        /// <summary>
        /// Updates the power level of all the weapons the player has
        /// </summary>
        public void UpdateWeapons()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.SetDamagePerShot(PowerLevel);
            }
            uppdatePowerlevel = false;
        }

        /// <summary>
        /// Below here are only properties of the player class
        /// </summary>
        public override float MaxHealth
        {
            get { return baseMaxHealth + Resources.SpHealth * 10; }
            protected set => base.MaxHealth = value;
        }

        public override float MaxShields
        {
            get { return baseMaxShields + Resources.SpShields * 10; }
            protected set => base.MaxShields = value;
        }

        public override float PowerLevel
        {
            get { return basePowerLevel + Resources.SpDamage; }
            protected set => base.PowerLevel = value;
        }
    }
}