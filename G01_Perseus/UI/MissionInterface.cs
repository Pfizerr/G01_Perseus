using G01_Perseus.EventSystem.Events;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace G01_Perseus.UI
{

    public class MissionInterface : GameState
    {
        private Rectangle bounds;
        private Texture2D background;
        private List<MissionInterfaceButton> buttons;
        private UIButton acceptMissionButton;
        private UIButton denyMissionButton;
        private bool isHovered;
        private float opacity;
        private MissionInterfaceButton selected;

        public MissionInterface(Mission[] missions, ExtendedTimer[] cooldowns)
        {
            this.background = AssetManager.TextureAsset("monitor_focused");
            this.Transparent = true;
            this.opacity = 1f;

            bounds = new Rectangle(Game1.camera.Viewport.Width / 2 - 780 / 2, Game1.camera.Viewport.Height / 2 - 580 / 2, 780, 580);

            CreateButtons(missions, cooldowns);

            selected = buttons[0];
            buttons[0].IsSelected = true;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MissionInterfaceButton button in buttons)
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

            foreach (MissionInterfaceButton button in buttons)
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

            buttons = new List<MissionInterfaceButton>();

            for (int i = 0; i < missions.Length; i++)
            {
                if (missions[i] == null)
                {
                    buttons.Add(new MissionInterfaceButton(new Rectangle(bounds.X + 125, bounds.Y + 65 + (85 * i), 550, 80), bgAvailable, bgSelected, bgUnavailable, null, timer[i], "", AssetManager.FontAsset("default_font")));
                    continue;
                }

                string missionText = "";

                if (missions[i] != null)
                {
                    missionText = missions[i].Text();
                }

                buttons.Add(new MissionInterfaceButton(new Rectangle(bounds.X + 125, bounds.Y + 65 + (85 * i), 550, 80), bgAvailable, bgSelected, bgUnavailable, missions[i], timer[i], missions[i].Text(), AssetManager.FontAsset("default_font")));
            }

            acceptMissionButton = new UIButton(new Rectangle(bounds.X + 499, bounds.Y + 481, 60, 59), () =>
            {
                Accept();
            });

            denyMissionButton = new UIButton(new Rectangle(bounds.X + 600, bounds.Y + 481, 60, 59), () =>
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
            if (selected != null && selected.Mission != null)
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