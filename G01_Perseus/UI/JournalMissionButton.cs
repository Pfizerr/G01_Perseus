using G01_Perseus.EventSystem.Events;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    public class JournalMissionButton
    {
        private UIButton turnInMissionButton;
        private UIButton removeMissionButton;

        private Mission mission;
        private bool isMissionCompleted;
        private Rectangle bounds;

        private string missionText;
        private SpriteFont font;

        public JournalMissionButton(Mission mission, Rectangle bounds)
        {
            this.mission = mission;
            this.bounds = bounds;

            this.missionText = mission.ToString();
            this.font = AssetManager.FontAsset("default_font_small");

            Texture2D backgroundAvailable = AssetManager.TextureAsset("button_background_available");
            Texture2D backgroundUnavailable = AssetManager.TextureAsset("button_background_unavailable");
            Texture2D backgroundSelected = AssetManager.TextureAsset("button_background_selected");

            //Remove-Button bounds
            Texture2D removeBackground = AssetManager.TextureAsset("button_red");
            int removeX = bounds.X + bounds.Width - (removeBackground.Width - 5);
            int removeY = bounds.Y + bounds.Height / 2 - (removeBackground.Height / 2);

            Rectangle removeMissionButtonHitbox = new Rectangle(removeX, removeY, removeBackground.Width, removeBackground.Height);
            removeMissionButton = new UIButton(removeMissionButtonHitbox, removeBackground, () =>
            {
                RemoveMission();
            });

            //Turn-In-Button bounds
            Texture2D turnInBackground = AssetManager.TextureAsset("button_green");
            int turnInX = bounds.X + bounds.Width - (turnInBackground.Width + 50);
            int turnInY = bounds.Y + bounds.Height / 2 - (turnInBackground.Height / 2);

            Rectangle acceptMissionButtonHitbox = new Rectangle(turnInX, turnInY, turnInBackground.Width, turnInBackground.Height);
            turnInMissionButton = new UIButton(acceptMissionButtonHitbox, turnInBackground, () =>
            {
                TurnInMission();
            });


            if(mission != null)
            {
                HasMission = true;
            }
        }

        public bool HasMission
        {
            get;
            private set;
        }

        public float Opacity
        {
            get;
            set;
        }

        public Rectangle Bounds => bounds;

        public void Update(GameTime gameTime)
        {
            if (mission == null)
            {
                HasMission = false;
            }

            if (mission.State == Mission.EState.Completed)
            {
                isMissionCompleted = true;

                turnInMissionButton.Update(gameTime);
            }

            if (isMissionCompleted)
            {
                turnInMissionButton.Update(gameTime);
            }
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
            Vector2 missionTextPosition = new Vector2(bounds.Center.X - missionTextSize.X / 2 - 60, bounds.Center.Y - missionTextSize.Y / 2);

            spriteBatch.DrawString(font, missionText, missionTextPosition, Color.White * Opacity, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);

            #region debug (draw hitbox)
            //spriteBatch.Draw(Util.CreateRectangleTexture(bounds.Width, bounds.Height, Color.Blue, Color.Transparent), bounds.Location.ToVector2(), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            #endregion
        }

        public void OffsetAlongY(int offset, bool lerpToTarget)
        {
            // bounds.Y += offset;
            //bounds.Location.
        }

        public int Y
        {
            get
            {
                return this.bounds.Y;
            }
            set
            {
                this.bounds.Y = value;
            }
        }

        public void TurnInMission()
        {
            EventManager.Dispatch(new MissionTurnedInEvent(mission));
            HasMission = false;
        }

        public void RemoveMission()
        {
            EventManager.Dispatch(new MissionRemovedEvent(mission));
            HasMission = false;
        }
    }
}
