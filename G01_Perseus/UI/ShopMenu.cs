using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using G01_Perseus.EventSystem.Events;


namespace G01_Perseus.UI
{
    class ShopMenu : GameState
    {
        private GameWindow window;
        private Texture2D panelTexture, trippleShotTexture, healthTexture, currencyTexture;
        private Menu menu;
        private int[] optionValue;
        private bool purchased;
        //private string currencyAmount;
        private Vector2 currencyAmountPos, panelTexturePos;
        private Rectangle trippleShootTexPos, healthTexPos, currencyTexPos;
        private SpriteFont testFont;

        public ShopMenu(GameWindow window)
        {
            this.window = window;
            this.Transparent = true;
            this.panelTexture = AssetManager.TextureAsset("shop_menu_background_UI");
            this.panelTexturePos = new Vector2(window.ClientBounds.Width / 2 - ((panelTexture.Width * 1.5f) / 2), window.ClientBounds.Height * 0.3f);
            menu = new Menu(new Rectangle((int)(window.ClientBounds.Width / 2 - ((panelTexture.Width) / 2)) + 100, (int)(window.ClientBounds.Height * 0.3f) + 50, (int)(panelTexture.Width) - 100, (int)(panelTexture.Height) - 100), AssetManager.FontAsset("main_menu_font"));
            menu.Options.Add(new MenuOption("Buy tripple shot, amount 100", BuyTrippleShot));
            menu.Options.Add(new MenuOption("Buy health: +10, amount 10", BuyHealth));
            optionValue = new int[] { 100, 10};
            purchased = false;

            testFont = AssetManager.FontAsset("main_menu_font");
            trippleShotTexture = AssetManager.TextureAsset("tripple_shot_icon");
            healthTexture = AssetManager.TextureAsset("health_icon");
            currencyTexture = AssetManager.TextureAsset("resource_currency");
            trippleShootTexPos = new Rectangle((int)panelTexturePos.X + 100, (int)panelTexturePos.Y + 80, trippleShotTexture.Width, trippleShotTexture.Height);
            healthTexPos = new Rectangle((int)panelTexturePos.X + 100, (int)(panelTexturePos.Y + 80 * 2.5f), trippleShotTexture.Width, trippleShotTexture.Height);
            currencyTexPos = new Rectangle((int)panelTexturePos.X + 100, (int)(panelTexturePos.Y + 80 * 4.5f), trippleShotTexture.Width, trippleShotTexture.Height);
            currencyAmountPos = new Vector2(currencyTexPos.X + currencyTexPos.Width + 10, currencyTexPos.Y + currencyTexPos.Height / 2 - testFont.MeasureString("10").Y/2);
        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                EventManager.Dispatch(new PopStateEvent());
            }
            //Debugging for fast coins
            if (KeyMouseReader.KeyPressed(Keys.G))
            {
                Resources.AddCurrency(100);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(panelTexture, panelTexturePos, null, Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.5f);
            menu.Draw(spriteBatch, gameTime);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), string.Format("{0}", Resources.Currency), currencyAmountPos, Color.Black);
            spriteBatch.Draw(trippleShotTexture, trippleShootTexPos, Color.White);
            spriteBatch.Draw(healthTexture, healthTexPos, Color.White);
            spriteBatch.Draw(currencyTexture, currencyTexPos, Color.White);
            spriteBatch.End();
        }

        private void BuyTrippleShot()
        {
            if (Resources.Currency >= optionValue[0] && !purchased)
            {
                Resources.DecreaseCurrency(optionValue[0]);
                purchased = true;
                EntityManager.Player.weapons[1].available = true;
                menu.Options[0].Text = "Out of stock";
                
            }            
        }

        private void BuyHealth()
        {
            if (Resources.Currency >= optionValue[1] && EntityManager.Player.Health < EntityManager.Player.MaxHealth)
            {
                Resources.DecreaseCurrency(optionValue[1]);
                if(EntityManager.Player.Health + 10 > EntityManager.Player.MaxHealth)
                {
                    EntityManager.Player.Health = EntityManager.Player.MaxHealth;
                }
                else
                {
                    EntityManager.Player.Health += 10;
                }
                EventManager.Dispatch(new HealthChangeEvent());
            }            
        }
    }
}
