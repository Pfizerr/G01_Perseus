﻿using G01_Perseus.EventSystem.Events;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Planet : EventListener, MissionAcceptedClickListener, MissionDeniedClickListener
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

        public Player Customer { get; set; }

        public Planet(string name, int maxNrOfMissions, Sprite highlightedSprite, Sprite sprite, Vector2 position)
        {
            this.name = name;
            this.maxNrOfMissions = maxNrOfMissions;
            this.highlightedSprite = highlightedSprite;
            this.sprite = sprite;
            this.position = position;

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
                missions[i] = MissionManager.LoadMission(MissionManager.GetRandomMissionId());
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
                    timer.Reset(Game1.random.Next(minMissionCooldown, maxMissionCooldown), false);
                    missions[i] = MissionManager.LoadMission(MissionManager.GetRandomMissionId());
                    missions[i].Contractor = this;
                    missions[i].State = Mission.EState.Offered;
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

                if (KeyMouseReader.LeftClick() /* && !isDisplayingUserInterface */)
                {
                    EventManager.Dispatch(new PlanetInteractionEvent(this));

                    // Open GUI
                    EventManager.Dispatch(new PushStateEvent(new MissionInterface(missions, missionCooldowns)));
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

        public Vector2 Center
        {
            get => position + origin;
            protected set => position = value - origin;
        }
    }
}