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
    public class NewJournal : GameState
    {
        private List<NewMissionButton> buttons;
        private Texture2D background;
        private Rectangle bounds;
        private float Opacity;

        public NewJournal(List<Mission> missions)
        {
            this.Transparent = true;
            this.Opacity = 1.0f;

            this.background = AssetManager.TextureAsset("journal_menu_background");
            this.bounds = new Rectangle(Game1.camera.Viewport.Width / 2 - background.Width / 2, Game1.camera.Viewport.Height / 2 - background.Height / 2, background.Width, background.Height);
            this.buttons = new List<NewMissionButton>();

            int bWidth = 500;
            int bHeight = 75;

            //foreach(Mission mission in missions)
            //{
             //   int verticalOffset = (buttons.Count * bHeight) + bHeight;
                //Rectangle buttonBounds = new Rectangle(bounds.X, verticalOffset, bWidth, bHeight);
             //   Rectangle bBounds = new Rectangle(bounds.X - (int)(bWidth / 2), bounds.Y - (int)(bHeight / 2), bWidth, bHeight);

              //  buttons.Add(new NewMissionButton(mission, bBounds));
            //}

            /*foreach(Mission mission in missions)
            {
                Rectangle bBounds = new Rectangle(bounds.Center.X - bWidth / 2, bounds.Center.Y - bHeight / 2, bWidth, bHeight);
                buttons.Add(new NewMissionButton(mission, bBounds));
            }*/

            for (int i = 0; i < missions.Count(); i++)
            {
                int y = (bounds.Y + 35) + (i * bHeight) + (i * 10);
                Rectangle bBounds = new Rectangle(bounds.Center.X - bWidth / 2, y, bWidth, bHeight);
                buttons.Add(new NewMissionButton(missions[i], bBounds));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();


            spriteBatch.Draw(background, bounds, Color.White * Opacity);

            foreach (NewMissionButton button in buttons)
            {
                button.Draw(spriteBatch, gameTime);
            }

            //spriteBatch.Draw(Util.CreateRectangleTexture(bounds.Width, bounds.Height, Color.Blue, Color.Transparent), bounds.Location.ToVector2(), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();

            foreach(NewMissionButton button in buttons)
            {
                button.Opacity = Opacity;
                button.Update(gameTime);
            }
        }

        public void HandleInput()
        {
            if(KeyMouseReader.KeyPressed(Keys.M))
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
