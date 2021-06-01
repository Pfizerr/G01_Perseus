using G01_Perseus.EventSystem.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    public class NewMissionButton
    {
        private UIButton turnInMissionButton;
        private UIButton removeMissionButton;

        private Mission mission;
        private bool isMissionCompleted;

        private Texture2D backgroundAvailable;
        private Texture2D backgroundUnavailable;
        private Texture2D backgroundSelected;
        private Rectangle bounds;

        private string missionText;
        private SpriteFont font;

        public NewMissionButton(Mission mission, Rectangle bounds)
        {
            this.mission = mission;
            this.bounds = bounds;

            this.missionText = mission.Text();
            this.font = AssetManager.FontAsset("default_font_small");

            this.backgroundAvailable = AssetManager.TextureAsset("button_background_available");
            this.backgroundUnavailable = AssetManager.TextureAsset("button_background_unavailable");
            this.backgroundSelected = AssetManager.TextureAsset("button_background_hovered");

            //Remove-Button bounds
            Texture2D removeBackground = AssetManager.TextureAsset("button_red");
            int removeX = bounds.X + bounds.Width - (removeBackground.Width + 20);
            int removeY = bounds.Y + bounds.Height / 2 - (removeBackground.Height / 2);
            Rectangle deleteHitbox = new Rectangle(removeX, removeY, removeBackground.Width, removeBackground.Height);

            removeMissionButton = new UIButton(deleteHitbox, removeBackground, () =>
            {
                RemoveMission();
            });

            //Turn-In-Button bounds
            Texture2D turnInBackground = AssetManager.TextureAsset("button_green");
            int turnInX = bounds.X + bounds.Width -  (turnInBackground.Width + 50);
            int turnInY = bounds.Y + bounds.Height / 2 - (turnInBackground.Height / 2);
            Rectangle acceptHitbox = new Rectangle(turnInX, turnInY, turnInBackground.Width, turnInBackground.Height);

            turnInMissionButton = new UIButton(acceptHitbox, turnInBackground, () =>
            {
                TurnInMission();
            });

        }

        public bool IsAlive
        {
            get;
            private set;
        }

        public float Opacity
        {
            get;
            set;
        }

        public void Update(GameTime gameTime)
        {
            if(mission == null)
            {
                IsAlive = false;
            }

            if(mission.State == Mission.EState.Completed)
            {
                isMissionCompleted = true;

                turnInMissionButton.Update(gameTime);
            }

            turnInMissionButton.Update(gameTime);
            removeMissionButton.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isMissionCompleted)
            {
                turnInMissionButton.Opacity = 1f;
            }
            else
            {
                turnInMissionButton.Opacity = 0.5f;
            }

            turnInMissionButton.Draw(spriteBatch, gameTime);
            removeMissionButton.Draw(spriteBatch, gameTime);

            Vector2 missionTextSize = font.MeasureString(missionText);
            Vector2 missionTextPosition = new Vector2(bounds.Center.X - missionTextSize.X  / 2 - 60, bounds.Center.Y - missionTextSize.Y / 2);

            spriteBatch.DrawString(font, missionText, missionTextPosition, Color.White * Opacity, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);

            //spriteBatch.Draw(Util.CreateRectangleTexture(bounds.Width, bounds.Height, Color.Blue, Color.Transparent), bounds.Location.ToVector2(), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }

        public void TurnInMission()
        {
            EventManager.Dispatch(new MissionTurnedInEvent(mission));
            IsAlive = false;
        }

        public void RemoveMission()
        {
            EventManager.Dispatch(new MissionRemovedEvent(mission));
            IsAlive = false;
        }
    }
}
