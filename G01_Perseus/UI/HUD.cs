using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G01_Perseus.EventSystem.Events;

namespace G01_Perseus.UI
{
    class HUD : GameState
    {

        private UIButton testButton;

        public HUD()
        {
            this.Transparent = true;
            this.testButton = new UIButton(new Rectangle(10, 10, 50, 50), AssetManager.TextureAsset("button_blue"), Test);
            testButton.HoveredTexture = AssetManager.TextureAsset("button_red");

        }

        public void Test()
        {
            EventManager.Dispatch(new PushStateEvent(new QuestLogInterface()));
            Console.WriteLine("Hello!!!");
        }

        public override void Update(GameTime gameTime)
        {
            testButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            testButton.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
