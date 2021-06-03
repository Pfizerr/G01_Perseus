using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Player : Ship, /*CollissionListener,*/ PlanetInteractionListener, MouseEnterPlanetListener, MouseExitPlanetListener
    {
        private bool hasFocusOnPlanet;
        private float baseMaxHealth;
        private float baseMaxShields;
        private float basePowerLevel;
        private float baseFireRate;
        //public enum Addons { Disruptor, LifeSteal, Piercing, Freeze}
        public List<Weapon> weapons;


        public List<Weapon> Weapons => weapons;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="maxVelocity"></param>
        /// <param name="scale"></param>
        /// <param name="health"></param>
        /// <param name="shield"></param>

        public Player(Vector2 position, Vector2 velocity, Vector2 scale, float health, float shield, Texture2D texture, float powerLevel, float fireRate) : base(position, velocity, scale, health, shield, texture, powerLevel, fireRate)
        {
            baseMaxHealth = health;
            baseMaxShields = shield;
            basePowerLevel = powerLevel;
            baseFireRate = fireRate;
            weapons = new List<Weapon>() { equipedWeapon, new WeaponTripleShot(1, basePowerLevel, baseFireRate, false) };
            //UpdateWeapons(); //May be needed when you load a save file
            EventManager.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            //These if statements should be moved perhaps?
            if (Shields > MaxShields)
            {
                Shields = MaxShields;
            }

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            
            AdjustAngleTowardsTarget(FindMousePosition());
            HandleInput(gameTime);
            base.Update(gameTime);

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

            if(KeyMouseReader.KeyPressed(Keys.J))
            {
                EventManager.Dispatch(new PushStateEvent(new Journal(Status.Missions)));
            }

            if(KeyMouseReader.LeftHold() && !hasFocusOnPlanet)
            {
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
            if (KeyMouseReader.KeyPressed(Keys.D1) && weapons[0].available) //KeyMouseReader.KeyPressed(Keys.None)
            {
                equipedWeapon = weapons[0];
                EventManager.Dispatch(new ChangeIconEvent(0));
            }

            if (KeyMouseReader.KeyPressed(Keys.D2) && weapons[1].available)
            {
                equipedWeapon = weapons[1];
                EventManager.Dispatch(new ChangeIconEvent(1));
            }

            if (KeyMouseReader.KeyPressed(Keys.D3) && weapons[2].available)
            {
                equipedWeapon = weapons[2];
                EventManager.Dispatch(new ChangeIconEvent(2));
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
                weapon.SetFireTimer(FireRate);
            }
        }

        public override void RecieveDamage(Entity other, float damage)
        {
            base.RecieveDamage(other, damage);
            EventManager.Dispatch(new HealthChangeEvent());
        }

        public override void ShieldRegeneration(GameTime gameTime)
        {
            base.ShieldRegeneration(gameTime);
            EventManager.Dispatch(new HealthChangeEvent());
        }

        /// <summary>
        /// Below here are only properties of the player class
        /// </summary>
        public override float MaxHealth
        {
            get { return baseMaxHealth + Resources.SpHealth * 15; }
            protected set => base.MaxHealth = value;
        }

        public override float MaxShields
        {
            get { return baseMaxShields + Resources.SpShields * 5; }
            protected set => base.MaxShields = value;
        }

        public override float PowerLevel
        {
            get { return basePowerLevel + Resources.SpDamage; }
            protected set => base.PowerLevel = value;
        }

        public override float FireRate
        {
            get { return baseFireRate + Resources.SpFireRate * 10; }
            protected set => base.FireRate = value;
        }
    }
}