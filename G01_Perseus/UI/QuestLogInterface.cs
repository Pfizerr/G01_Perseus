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

        private UIButton exit, addDamage, addShields, addHealth, addFireRate;
        //private List<UIButton> buttons;
        private UIButton[] buttonsArray;
        private List<Rectangle> skillBarPos;

        public QuestLogInterface(GameWindow window)
        {
            this.Transparent = true;

            this.bounds = new Rectangle(window.ClientBounds.Width / 2 - 200, 200, 400, 400);

            this.backgroundTexture = Util.CreateFilledRectangleTexture(new Color(255, 255, 255, 255), 1, 1);
            this.exit = new UIButton(new Rectangle(bounds.X + 10, bounds.Y + 10, 20, 20), ExitUI);
            this.addDamage = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 60, 50, 50), Resources.AddSpDamage);
            this.addHealth = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 120, 50, 50), Resources.AddSpHealth);
            this.addShields = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 180, 50, 50), Resources.AddSpShields);
            this.addFireRate = new UIButton(new Rectangle(bounds.X + bounds.Width - 60, bounds.Y + 240, 50, 50), Resources.AddSpFireRate);
            buttonsArray = new UIButton[] { addDamage, addHealth, addShields, addFireRate};

            skillBarPos = new List<Rectangle>();
            for (int i = 0; i < buttonsArray.Length; i++)
            {
                //Note to self: 60 is the space for the icon/text to the left and thuss you want 70 to the right not to conncet with the button on the right
                skillBarPos.Add(new Rectangle(bounds.X + 60, buttonsArray[i].Hitbox.Y, buttonsArray[i].Hitbox.X - (bounds.X + 70), buttonsArray[i].Hitbox.Height));
            }
        }

        public void ExitUI()
        {
            EventManager.Dispatch(new PopStateEvent());
        }

        public override void Update(GameTime gameTime)
        {
            exit.Update(gameTime);
            foreach(UIButton button in buttonsArray)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, bounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            exit.Draw(spriteBatch, gameTime);
            foreach(UIButton button in buttonsArray)
            {
                button.Draw(spriteBatch, gameTime);
            }
            foreach (Rectangle position in skillBarPos)
            {
                spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), position, Color.Green);
            }
            spriteBatch.End();
        }
    }
}
