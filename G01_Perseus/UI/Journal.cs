using G01_Perseus.EventSystem.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace G01_Perseus.UI
{
    public class Journal : GameState
    {
        private List<JournalMissionButton> buttons;
        private Texture2D background;
        private Rectangle bounds;
        private float Opacity;
        private int buttonWidth;
        private int buttonHeight;

        public Journal(List<Mission> missions)
        {
            this.Transparent = true;
            this.Opacity = 1.0f;

            this.background = AssetManager.TextureAsset("journal_menu_background");
            this.bounds = new Rectangle(Game1.camera.Viewport.Width / 2 - background.Width / 2, Game1.camera.Viewport.Height / 2 - background.Height / 2, background.Width, background.Height);
            this.buttons = new List<JournalMissionButton>();

            buttonWidth = 500;
            buttonHeight = 75;

            for (int i = 0; i < missions.Count; i++)
            {
                int y = (bounds.Y + 35) + (i * buttonHeight) + (i * 10);
                Rectangle bBounds = new Rectangle(bounds.Center.X - buttonWidth / 2, y, buttonWidth, buttonHeight);
                buttons.Add(new JournalMissionButton(missions[i], bBounds));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();


            spriteBatch.Draw(background, bounds, Color.White * Opacity);

            foreach (JournalMissionButton button in buttons)
            {
                button.Draw(spriteBatch, gameTime);
            }

            #region debug (draw hitbox)
            //spriteBatch.Draw(Util.CreateRectangleTexture(bounds.Width, bounds.Height, Color.Blue, Color.Transparent), bounds.Location.ToVector2(), Color.White);
            #endregion

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();

            foreach (JournalMissionButton button in buttons)
            {
                button.Opacity = Opacity;
                button.Update(gameTime);


            }

            for(int i = 0; i < buttons.Count; i++)
            {
                if(buttons[i].HasMission == false)
                {
                    buttons.RemoveAt(i);
                    continue;
                }

                buttons[i].Opacity = Opacity;
                buttons[i].Update(gameTime);
            }
        }

        public void UpdatePositions()
        {
            int emptySlots = 0;
            for(int i = 0; i < buttons.Count; i++)
            {
                Rectangle bounds = buttons[i].Bounds;
                buttons[i].Y = (bounds.Y + 35) + (i - emptySlots * buttonHeight) + ((i - emptySlots) * 10);
            }
        }

        public void HandleInput()
        {
            if (KeyMouseReader.KeyPressed(Keys.M))
            {
                EventManager.Dispatch(new PopStateEvent());
            }

            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                EventManager.Dispatch(new PopStateEvent());
            }

            if (KeyMouseReader.MouseScreenPosition.X < bounds.Left
                || KeyMouseReader.MouseScreenPosition.X > bounds.Right
                || KeyMouseReader.MouseScreenPosition.Y < bounds.Top
                || KeyMouseReader.MouseScreenPosition.Y > bounds.Bottom)
            {
                Opacity = 0.5f;

                if (KeyMouseReader.LeftClick() || KeyMouseReader.RightClick())
                {
                    EventManager.Dispatch(new PopStateEvent());
                }
            }
            else
            {
                Opacity = 1.0f;
            }
        }
    }
}