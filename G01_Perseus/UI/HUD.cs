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
        private QuestLogInterface questLogInterface;
        private GameWindow window;
        private float healthbarWidth;
        private float shieldbarWidth;
        private Rectangle healthbarSize, shieldbarSize;
        private float healthbarHeight;
        private Texture2D barTex;
        private Vector2 shieldNrPos, healthNrPos;

        public HUD(GameWindow window)
        {
            this.Transparent = true;
            this.testButton = new UIButton(new Rectangle(10, 10, 50, 50), AssetManager.TextureAsset("button_blue"), Test);
            testButton.HoveredTexture = AssetManager.TextureAsset("button_red");
            this.questLogInterface = new QuestLogInterface(window);
            this.window = window;
            barTex = AssetManager.TextureAsset("gradient_bar");
            healthbarWidth = barTex.Width;
            shieldbarWidth = barTex.Width;
            healthbarHeight = barTex.Height / 2;
            healthbarHeight = 40;
            healthbarSize = new Rectangle((int)healthbarHeight, window.ClientBounds.Height - 40, barTex.Width, barTex.Height / 2);
            shieldbarSize = new Rectangle((int)healthbarHeight, window.ClientBounds.Height - 80, barTex.Width, barTex.Height / 2);
            //Add text to the healthbars for how much you have left
            float offsett = 10;
            healthNrPos = new Vector2(healthbarSize.X + barTex.Width + offsett, healthbarSize.Y);
            shieldNrPos = new Vector2(shieldbarSize.X + barTex.Width + offsett, shieldbarSize.Y);


        }

        public void Test()
        {
            EventManager.Dispatch(new PushStateEvent(questLogInterface));
            Console.WriteLine("Hello!!!");
        }

        public override void Update(GameTime gameTime)
        {
            testButton.Update(gameTime);
            UpdatePlayerHealth();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            testButton.Draw(spriteBatch, gameTime);
            spriteBatch.Draw(barTex, healthbarSize, Color.Crimson);
            spriteBatch.Draw(barTex, shieldbarSize, Color.Cyan);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), EntityManager.Player.Health.ToString(), healthNrPos, Color.Crimson);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), EntityManager.Player.Shields.ToString(), shieldNrPos, Color.Cyan);
            spriteBatch.End();
        }

        public void UpdatePlayerHealth()
        {
            //Note for the % version of this part of the HUD. Replace the Entinty.Player.Max... with EntityManager.Player.TotalHealth
            healthbarSize.Width = (int)((EntityManager.Player.Health / EntityManager.Player.MaxHealth) * barTex.Width);
            shieldbarSize.Width = (int)((EntityManager.Player.Shields / EntityManager.Player.MaxShields) * barTex.Width);
        }
    }
}
