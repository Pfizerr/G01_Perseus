using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    class ShopMenu : GameState
    {
        private GameWindow window;
        private Texture2D panelTexture;
        private Menu menu;
        private int[] optionValue;
        private bool purchased;

        public ShopMenu(GameWindow window)
        {
            this.window = window;
            this.Transparent = true;
            this.panelTexture = AssetManager.TextureAsset("shop_menu_background_UI");

            menu = new Menu(new Rectangle((int)(window.ClientBounds.Width / 2 - ((panelTexture.Width * 1.5f) / 2)) + 50, (int)(window.ClientBounds.Height * 0.3f) + 50, (int)(panelTexture.Width * 1.5f) - 100, (int)(panelTexture.Height * 1.5f) - 100), AssetManager.FontAsset("main_menu_font"));
            menu.Options.Add(new MenuOption("Buy tripple shot", BuyTrippleShot));
            menu.Options.Add(new MenuOption("Buy health", BuyHealth));
            optionValue = new int[] { 100, 10};
            purchased = false;

        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(panelTexture, new Vector2(window.ClientBounds.Width / 2 - ((panelTexture.Width * 1.5f) / 2), window.ClientBounds.Height * 0.3f), null, Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.5f);
            menu.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        private void BuyTrippleShot() //Give an input parameter how??
        {
            if (Resources.Currency >= optionValue[0] && !purchased)
            {
                Resources.DecreaseCurrency(optionValue[0]);
                purchased = true;
                EntityManager.Player.weaponStatuses[1] = Player.WeaponStatus.Available; //Distaptch to the player that tripple shot is aviable
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
                    //EntityManager.Player.Health = EntityManager.Player.MaxHealth;
                }
                else
                {
                    //EntityManager.Player.Health += 10;
                }
                EventManager.Dispatch(new HealthChangeEvent());
            }
            
        }
    }
}
