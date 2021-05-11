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
    public class QuestLogInterface : GameState
    {

        private Rectangle bounds;
        private Texture2D backgroundTexture;

        private UIButton exit, addDamage, addShields, addHealth;
        //private List<UIButton> buttons;
        private UIButton[] buttonsArray;

        public QuestLogInterface(GameWindow window)
        {
            this.Transparent = true;

            this.bounds = new Rectangle(window.ClientBounds.Width / 2 - 200, 200, 400, 400);

            this.backgroundTexture = Util.CreateFilledRectangleTexture(new Color(255, 255, 255, 255), 1, 1);
            this.exit = new UIButton(new Rectangle(bounds.X + 10, bounds.Y + 10, 20, 20), ExitUI);
            this.addDamage = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 60, 50, 50), Resources.AddSpDamage);
            this.addHealth = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 120, 50, 50), Resources.AddSpHealth);
            this.addShields = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 180, 50, 50), Resources.AddSpShields);
            buttonsArray = new UIButton[] { exit, addDamage, addHealth, addShields};


        }

        public void ExitUI()
        {
            EventManager.Dispatch(new PopStateEvent());
        }

        public override void Update(GameTime gameTime)
        {
            foreach(UIButton button in buttonsArray)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, bounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            foreach(UIButton button in buttonsArray)
            {
                button.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
    }
}
