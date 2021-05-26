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
    public class MissionLogInterface : GameState
    {

        private Rectangle bounds;
        private Texture2D backgroundTexture;

        private UIButton exitButton;

        public MissionLogInterface()
        {
            this.Transparent = true;

            this.bounds = new Rectangle(450,200, 250, 400);

            this.backgroundTexture = Util.CreateFilledRectangleTexture(new Color(255, 255, 255, 255), 1, 1);
            this.exitButton = new UIButton(new Rectangle(bounds.X + 10, bounds.Y + 10, 50, 50), () => 
            {
                EventManager.Dispatch(new PopStateEvent());
            });
        }

        public override void Update(GameTime gameTime)
        {
            exitButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, bounds, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            exitButton.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
