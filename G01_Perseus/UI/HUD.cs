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
    class HUD : GameState, HealthChangeListener
    {
        private UIButton btnSkillUI, btnShopUI;
        private SkillInterface skillInterface;
        private ShopMenu shopMenu;
        private GameWindow window;
        private float healthbarWidth;
        private float shieldbarWidth;
        private float healthbarHeight;
        private Rectangle healthbarSize, shieldbarSize, xpBarSize, xpBarOutline;        
        private Texture2D barTex, outlineTex;
        private Vector2 shieldNrPos, healthNrPos, levelTextPos, xpTextPos, btnSkillTextPos, btnShopTextPos, weponTextPos, weaponIconPos;
        private string levelText, xpText, btnSkillText, btnShopText, weaponText;

        public HUD(GameWindow window)
        {
            float offsett = 10;
            this.window = window;
            this.Transparent = true;

            //Buttons on UI
            SetButtons();
            this.skillInterface = new SkillInterface(window);
            this.shopMenu = new ShopMenu(window);

            //Healthbar portion
            barTex = AssetManager.TextureAsset("gradient_bar");
            SetHealthBarPositions(offsett);

            SetLevelAndXpBar(offsett);

            weaponText = "Weapon in use: ";
            EventManager.Register(this);
        }

        public void OpenSkillUI()
        {
            EventManager.Dispatch(new PushStateEvent(skillInterface));
            Console.WriteLine("Skill menu has been opened"); //Only for testing
        }

        public void OpenShopMenu()
        {
            EventManager.Dispatch(new PushStateEvent(shopMenu));
            Console.WriteLine("Shop menu has been opened"); //Only for testing
        }

        public override void Update(GameTime gameTime)
        {
            btnSkillUI.Update(gameTime);
            btnShopUI.Update(gameTime);
            //UpdatePlayerHealth();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            btnSkillUI.Draw(spriteBatch, gameTime);
            btnShopUI.Draw(spriteBatch, gameTime);
            spriteBatch.Draw(barTex, healthbarSize, Color.Crimson);
            spriteBatch.Draw(barTex, shieldbarSize, Color.Cyan);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), EntityManager.Player.Health.ToString(), healthNrPos, Color.Crimson);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), EntityManager.Player.Shields.ToString(), shieldNrPos, Color.Cyan);

            spriteBatch.Draw(barTex, xpBarSize, Color.Yellow);
            spriteBatch.Draw(outlineTex, xpBarOutline, Color.White);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), string.Format("Level: {0}", Resources.Level), levelTextPos, Color.Yellow);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), string.Format("{0} / {1}", Resources.XP, Resources.XPToNextLevel), xpTextPos, Color.Yellow);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), btnShopText, btnShopTextPos, Color.LightGreen);
            spriteBatch.DrawString(AssetManager.FontAsset("default_font"), btnSkillText, btnSkillTextPos, Color.LightBlue);
            spriteBatch.End();
        }

        public void UpdatePlayerHealth() //Make this an event when the player takes damage and call this method
        {
            //Note for the % version of this part of the HUD. Replace the Entinty.Player.Max... with EntityManager.Player.TotalHealth
            healthbarSize.Width = (int)((EntityManager.Player.Health / EntityManager.Player.MaxHealth) * barTex.Width);
            shieldbarSize.Width = (int)((EntityManager.Player.Shields / EntityManager.Player.MaxShields) * barTex.Width);

            xpBarSize.Width = (int)((Resources.XP / Resources.XPToNextLevel) * barTex.Width);
        }

        public void SetHealthBarPositions(float offsett)
        {
            healthbarWidth = barTex.Width;
            shieldbarWidth = barTex.Width;
            healthbarHeight = 40;
            healthbarSize = new Rectangle((int)healthbarHeight, window.ClientBounds.Height - 40, barTex.Width, barTex.Height / 2);
            shieldbarSize = new Rectangle((int)healthbarHeight, window.ClientBounds.Height - 80, barTex.Width, barTex.Height / 2);

            //Text            
            healthNrPos = new Vector2(healthbarSize.X + barTex.Width + offsett, healthbarSize.Y);
            shieldNrPos = new Vector2(shieldbarSize.X + barTex.Width + offsett, shieldbarSize.Y);
        }

        public void SetLevelAndXpBar(float offsett)
        {
            xpBarSize = new Rectangle(window.ClientBounds.Width - barTex.Width - 40, 40, (int)((Resources.XP / Resources.XPToNextLevel) * barTex.Width), barTex.Height / 3);
            xpBarOutline = new Rectangle(xpBarSize.X, xpBarSize.Y, barTex.Width, xpBarSize.Height);
            outlineTex = Util.CreateRectangleTexture(xpBarOutline.Width, xpBarOutline.Height, Color.White, Color.Transparent);
            levelText = string.Format("Level: {0}", Resources.Level);
            levelTextPos = new Vector2(xpBarSize.X - AssetManager.FontAsset("default_font").MeasureString(levelText).X - offsett, xpBarSize.Y);
            xpText = string.Format("{0} / {1}", Resources.XP, Resources.XPToNextLevel);
            xpTextPos = new Vector2(xpBarSize.X + barTex.Width / 2 - AssetManager.FontAsset("default_font").MeasureString(xpText).X / 2, xpBarSize.Y + AssetManager.FontAsset("default_font").MeasureString(xpText).Y + offsett);
        }

        public void SetButtons()
        {
            btnSkillText = "Skills";
            btnShopText = "Shop";
            this.btnSkillUI = new UIButton(new Rectangle((int)(window.ClientBounds.Width - 60), (int)(window.ClientBounds.Height - 60), 50, 50), AssetManager.TextureAsset("button_blue"), OpenSkillUI);
            this.btnShopUI = new UIButton(new Rectangle((int)(btnSkillUI.Hitbox.X - 60), (int)(btnSkillUI.Hitbox.Y), 50, 50), AssetManager.TextureAsset("button_green"), OpenShopMenu);
            btnSkillUI.HoveredTexture = AssetManager.TextureAsset("button_red");
            btnShopUI.HoveredTexture = AssetManager.TextureAsset("button_red");
            btnSkillTextPos = new Vector2(btnSkillUI.Hitbox.X, btnSkillUI.Hitbox.Y - AssetManager.FontAsset("default_font").MeasureString(btnSkillText).Y);
            btnShopTextPos = new Vector2(btnShopUI.Hitbox.X, btnShopUI.Hitbox.Y - AssetManager.FontAsset("default_font").MeasureString(btnShopText).Y);
        }

        public void HealthChange(HealthChangeEvent e)
        {
            healthbarSize.Width = (int)((EntityManager.Player.Health / EntityManager.Player.MaxHealth) * barTex.Width);
            shieldbarSize.Width = (int)((EntityManager.Player.Shields / EntityManager.Player.MaxShields) * barTex.Width);
        }
    }
}
