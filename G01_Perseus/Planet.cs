using Microsoft.Xna.Framework.Graphics;
using G01_Perseus.EventSystem.Events;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using G01_Perseus.UI;
using System.Linq;
using System;
using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus
{
    public class Planet : EventListener, MissionAcceptedClickListener, MissionDeniedClickListener, PopStateListener
    {
        private string name;
        private float radius;
        private float elapsedTime;
        private bool isHighlighted;
        private int maxNrOfMissions;
        private int minMissionCooldown;
        private int maxMissionCooldown;
        private ExtendedTimer[] missionCooldowns;
        private Mission[] missions;
        private Vector2 highlightedSpriteOffset;
        private Vector2 highlightedSpriteOrigin;
        private Sprite highlightedSprite;
        private Sprite sprite;
        private Vector2 position;
        private Vector2 origin;
        private Vector2 scale;
        private SpriteFont font;
        private bool isDisplayingUI;

        public Player Customer { get; set; }

        public Planet(string name, int maxNrOfMissions, Sprite highlightedSprite, Sprite sprite, Vector2 position)
        {
            this.name = name;
            this.maxNrOfMissions = maxNrOfMissions;
            this.highlightedSprite = highlightedSprite;
            this.sprite = sprite;
            this.position = position;

            font = AssetManager.FontAsset("default_font");
            isHighlighted = false;

            missions = new Mission[maxNrOfMissions];
            missionCooldowns = new ExtendedTimer[maxNrOfMissions];
            minMissionCooldown = 20000; /* ms */
            maxMissionCooldown = 30000; /* ms */

            scale = Vector2.One;
            origin = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            radius = (sprite.Width * 0.5f) - 1f;

            EventManager.Register(this);
        
            if (scale.X == scale.Y)
            {
                radius = (float)Math.Floor((decimal)sprite.Width / 2);
            }
            else { throw new NotImplementedException(); }
            
            highlightedSpriteOrigin = new Vector2(highlightedSprite.Width * 0.5f, highlightedSprite.Height * 0.5f);
            highlightedSpriteOffset = new Vector2((highlightedSprite.Width - sprite.Width) * 0.5f, (highlightedSprite.Height - sprite.Height) * 0.5f);

            for (int i = 0; i < missionCooldowns.Length; i++)
            {
                if (missionCooldowns[i] == null)
                {
                    missionCooldowns[i] = new ExtendedTimer(Game1.random.Next(minMissionCooldown, maxMissionCooldown), false);
                }
            }

            for (int i = 0; i < missions.Length; i++)
            {
                int rand = MissionManager.GetRandomMissionId();
                missions[i] = MissionManager.LoadMission(rand);
                if(missions[i] == null)
                {
                    continue;
                }
                missions[i].Contractor = this;
                missions[i].State = Mission.EState.Offered;
            }
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < missionCooldowns.Length; i++)
            {
                ExtendedTimer timer = missionCooldowns[i];
               
                if (timer.IsCounting)
                {
                    timer.Update(gameTime);
                }
                else if (timer.IsDoneCounting)
                {
                    while(missions[i] == null)
                    {
                        missions[i] = MissionManager.LoadMission(MissionManager.GetRandomMissionId());
                    }
                    missions[i].Contractor = this; //There is an error here when you close the game. Could be handles with a save and load function
                    //Though note that this error doesn't happen all the time
                    missions[i].State = Mission.EState.Offered;
                    timer.Reset(Game1.random.Next(minMissionCooldown, maxMissionCooldown), false);
                }
                else
                { /* Do nothing */}
            }

            HandleInteractions();
        }

        public void HandleInteractions()
        {
            if (KeyMouseReader.MouseWorldPosition.X < (Center.X + radius)
                && KeyMouseReader.MouseWorldPosition.X > (Center.X - radius)
                && KeyMouseReader.MouseWorldPosition.Y < (Center.Y + radius)
                && KeyMouseReader.MouseWorldPosition.Y > (Center.Y - radius))
            {
                isHighlighted = true;
                EventManager.Dispatch(new MouseEnterPlanetEvent(this));

                // Display open menu text

                if (KeyMouseReader.KeyHold(Keys.E) && !isDisplayingUI)
                {
                    EventManager.Dispatch(new PlanetInteractionEvent(this));

                    // Open GUI
                    EventManager.Dispatch(new PushStateEvent(new MissionInterface(missions, missionCooldowns)));
                    isDisplayingUI = true;
                }
            }
            else
            {
                if (isHighlighted)
                {
                    isHighlighted = false;
                    EventManager.Dispatch(new MouseExitPlanetEvent());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Center, scale, origin, 0f, 0.7f);
            if(isHighlighted)
            {
                highlightedSprite.Draw(spriteBatch, Center - (highlightedSpriteOffset / 75), scale, highlightedSpriteOrigin, 0f, 0.6f);
                string text = "Press 'e' to browse new missions";
                Vector2 textSize = font.MeasureString(text);
                spriteBatch.DrawString(font, "Press 'e' to browse new missions", KeyMouseReader.MouseWorldPosition, Color.Yellow, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
            }
        }

        public void OnDenied(MissionDeniedClickEvent e)
        {
            for (int i = 0; i < missions.Count(); i++)
            {
                if (missions[i] == e.Mission)
                {
                    missions[i] = null;
                    missionCooldowns[i].Start();
                    Console.WriteLine("New timer duration: " + missionCooldowns[i].Duration);
                }
            }
        }

        public void OnAccepted(MissionAcceptedClickEvent e)
        {
            for (int i = 0; i < missions.Count(); i++)
            {
                if (missions[i] == e.Mission && e.Mission.Contractor == this)
                {
                    Customer.RecieveMission(missions[i]);
                    missions[i] = null;
                    missionCooldowns[i].Start();
                    Console.WriteLine("New timer duration: " + missionCooldowns[i].Duration);
                    break;
                }
            }
        }

        public void OnPopState(PopStateEvent e)
        {
            if(isDisplayingUI)
            {
                isDisplayingUI = false;
            }
        }

        public Vector2 Center
        {
            get => position + origin;
            protected set => position = value - origin;
        }
    }
}
