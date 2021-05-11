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
        public Player(Vector2 position, Vector2 velocity, Vector2 scale, float health, float shield) : base(position, velocity, scale, health, shield)
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

        public override void HandleCollision(Entity other)
        {
            if (other is Enemy enemy)
            {
                //RecieveDamage(enemy.damage);
            }
            else if (other is Bullet bullet)
            {
                RecieveDamage(other, bullet.damage);
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
            Status.SkillPoints += skillPointRewards;
            Status.Resources += resourceRewards;
            Status.Dust += dustRewards;
        }

        public void RecieveMission(Mission mission) => Status.Missions.Add(mission);

        protected override void DefaultTexture()
        {
            texture = AssetManager.TextureAsset("player_ship");
        }

        public void Collision(CollissionEvent e)
        {
            HandleCollision(e.OtherEntity);
        }

        public override void Destroy(Event e)
        {
            base.Destroy(e);
            return;
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
               
            e.Planet.EnterNegotiation(this, completedMissions/*TEMP*/);
        }

        public void OnMouseEnter(MouseEnterPlanetEvent e)
        {
            hasFocusOnPlanet = true;
        }

        public void OnMouseExit(MouseExitPlanetEvent e)
        {
            hasFocusOnPlanet = false;
        }
    }
}