using G01_Perseus.EventSystem.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    public class MissionButton
    {
        private Texture2D background;
        private Texture2D backgroundSelected;
        private Texture2D backgroundAvailable;
        private Texture2D backgroundUnavailable;

        public MissionButton(Rectangle bounds, Texture2D backgroundAvailable, Texture2D backgroundSelected, Texture2D backgroundUnavailable, Mission mission, ExtendedTimer timer, string text, SpriteFont font)
        {
            this.background = backgroundAvailable;
            this.backgroundSelected = backgroundSelected;
            this.backgroundAvailable = backgroundAvailable;
            this.backgroundUnavailable = backgroundUnavailable;

            Mission = mission;
            Timer = timer;

            Button = new UIButton(bounds, backgroundAvailable, text, font, () =>
            {
                IsSelected = true;
            });

            Button.HoveredTexture = backgroundSelected;
        }

        public MissionButton(UIButton button, Mission mission, ExtendedTimer timer)
        {
            Button = button;
            Mission = mission;
            Timer = timer;
        }

        public Mission Mission 
        { 
            get; 
            private set; }

        public ExtendedTimer Timer 
        { 
            get;
            private set;
        }

        public UIButton Button 
        { 
            get; 
            private set;
        }

        public bool IsHovered
        {
            get;
            private set;
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public float Opacity
        {
            get;
            set;
        }

        public void Update(GameTime gameTime)
        {
            if(Button.Opacity != Opacity)
            {
                Button.Opacity = Opacity;
            }



            Button.Update(gameTime);
            Timer.Update(gameTime);

            if(Mission != null)
            {
                Mission.Update();
            }
        }

        private void UpdateTexture()
        {
            if (Mission == null || Mission.State != Mission.EState.Offered)
            {
                Button.Texture = backgroundUnavailable;
                Button.HoveredTexture = backgroundUnavailable;
                Button.Text = Convert.ToInt32(Timer.TimeRemaining / 1000).ToString() + " s"; // Update button text with current timer time.
            }
            else if (IsSelected)
            {
                Button.Texture = backgroundSelected;
            }
            else
            {
                Button.Texture = backgroundAvailable;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime) => Button.Draw(spriteBatch, gameTime);

    public class NewMissionInterface : GameState
    {
        private Rectangle bounds;
        private Texture2D background;
        private List<MissionButton> buttons;
        private UIButton acceptMissionButton;
        private UIButton denyMissionButton;
        private bool isHovered;
        private float opacity;
        private MissionButton selected;
        private SpriteFont font;

        public NewMissionInterface(Mission[] missions, ExtendedTimer[] cooldowns)
        {
            this.Transparent = true;
            this.opacity = 1f;

            this.background = AssetManager.TextureAsset("monitor_focused");
            this.font = AssetManager.FontAsset("default_font");

            bounds = new Rectangle(Game1.camera.Viewport.Width / 2 - 780 / 2, Game1.camera.Viewport.Height / 2 - 580 / 2, 780, 580);

            CreateButtons(missions, cooldowns);

            selected = buttons[0];
            buttons[0].IsSelected = true;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MissionButton button in buttons)
            {
                button.Update(gameTime);


                if (button.IsSelected && button != selected)
                {
                    selected.IsSelected = false;
                    selected = button;
                }
            }

            acceptMissionButton.Update(gameTime);
            denyMissionButton.Update(gameTime);
            HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, bounds, null, Color.White * opacity, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);

            foreach (MissionButton button in buttons)
            {
                button.Draw(spriteBatch, gameTime);
                button.Opacity = isHovered ? 1.0f : 0.5f;
            }

            opacity = isHovered ? 1.0f : 0.5f;

            spriteBatch.End();
        }

        private void HandleInput()
        {
            UpdateFocus();

            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                EventManager.Dispatch(new PopStateEvent());
            }

            if (!isHovered)
            {
                if (KeyMouseReader.LeftClick() || KeyMouseReader.RightClick())
                {
                    EventManager.Dispatch(new PopStateEvent());
                }
            }
        }

        private void CreateButtons(Mission[] missions, ExtendedTimer[] timer)
        {
            Texture2D bgAvailable = AssetManager.TextureAsset("button_background_available");
            Texture2D bgSelected = AssetManager.TextureAsset("button_background_hovered");
            Texture2D bgUnavailable = AssetManager.TextureAsset("button_background_unavailable");

            buttons = new List<MissionButton>();

            for (int i = 0; i < missions.Count(); i++)
            {
                if (missions[i] == null)
                {
                    buttons.Add(new MissionButton(new Rectangle(bounds.X + 125, bounds.Y + 65 + (85 * i), 550, 80), bgAvailable, bgSelected, bgUnavailable, null, timer[i], "", AssetManager.FontAsset("default_font")));
                    continue;
                }

                string missionText = "";

                if (missions[i] != null)
                {
                    missionText = missions[i].Text();
                }

                buttons.Add(new MissionButton(new Rectangle(bounds.X + 125, bounds.Y + 65 + (85 * i), 550, 80), bgAvailable, bgSelected, bgUnavailable, missions[i], timer[i], missions[i].Text(), AssetManager.FontAsset("default_font")));
            }

            acceptMissionButton = new UIButton(new Rectangle(bounds.X + 499, bounds.Y + 481, 60, 59), null, "T1", font, () =>
            {
                Accept();
            });

            denyMissionButton = new UIButton(new Rectangle(bounds.X + 600, bounds.Y + 481, 60, 59), null, "T2", font, () =>
            {
                Deny();
            });
        }

        public void UpdateFocus()
        {
            if (KeyMouseReader.MouseScreenPosition.X < bounds.Left
                || KeyMouseReader.MouseScreenPosition.X > bounds.Right
                || KeyMouseReader.MouseScreenPosition.Y < bounds.Top
                || KeyMouseReader.MouseScreenPosition.Y > bounds.Bottom)
            { 
                isHovered = false;
            }
            else
            {
                isHovered = true;
            }
        }

        private void Accept()
        {
            if(selected != null && selected.Mission != null)
            {
                EventManager.Dispatch(new MissionAcceptedClickEvent(selected.Mission));
            }
        }

        private void Deny()
        {
            if (selected != null && selected.Mission != null)
            {
                EventManager.Dispatch(new MissionDeniedClickEvent(selected.Mission));
            }
        }
    }
}
