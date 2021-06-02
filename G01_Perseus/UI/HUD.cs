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
    class HUD : GameState, HealthChangeListener, GainXpListener
    {
        private UIButton btnSkillUI, btnShopUI;
        private SkillInterface skillInterface;
        private ShopMenu shopMenu;
        private GameWindow window;
        private SpriteFont fontHUD;
        private float healthbarWidth;
        private float shieldbarWidth;
        private int healthbarHeight;
        private Rectangle healthbarSize, shieldbarSize, xpBarSize, xpBarOutline;        
        private Texture2D barTex, outlineTex, equipedWeaponIcon;
        private Vector2 shieldNrPos, healthNrPos, levelTextPos, xpTextPos, btnSkillTextPos, btnShopTextPos, weaponTextPos, weaponIconPos;
        private string levelText, xpText, btnSkillText, btnShopText, weaponText;

        public HUD(GameWindow window)
        {
            float offsett = 10;
            this.window = window;
            this.Transparent = true;
            this.fontHUD = AssetManager.FontAsset("default_font");
            this.equipedWeaponIcon = AssetManager.TextureAsset("projectile_green"); 
            //Buttons on UI
            SetButtons();
            this.skillInterface = new SkillInterface(window);
            this.shopMenu = new ShopMenu(window);

            //Healthbar portion
            barTex = AssetManager.TextureAsset("gradient_bar");
            SetHealthBarPositions(offsett);

            SetLevelAndXpBar(offsett);

            weaponText = "Weapon in use: ";
            weaponIconPos = new Vector2(window.ClientBounds.Width / 2, window.ClientBounds.Height - equipedWeaponIcon.Height * 0.5f - offsett);
            weaponTextPos = new Vector2(weaponIconPos.X - fontHUD.MeasureString(weaponText).X - offsett, weaponIconPos.Y + (equipedWeaponIcon.Height * 0.5f) / 2 - fontHUD.MeasureString(weaponText).Y / 2);
            
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
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            btnSkillUI.Draw(spriteBatch, gameTime);
            btnShopUI.Draw(spriteBatch, gameTime);
            spriteBatch.Draw(barTex, healthbarSize, Color.Crimson);
            spriteBatch.Draw(barTex, shieldbarSize, Color.Cyan);
            spriteBatch.DrawString(fontHUD, EntityManager.Player.Health.ToString(), healthNrPos, Color.Crimson);
            spriteBatch.DrawString(fontHUD, EntityManager.Player.Shields.ToString(), shieldNrPos, Color.Cyan);

            spriteBatch.Draw(barTex, xpBarSize, Color.Yellow);
            spriteBatch.Draw(outlineTex, xpBarOutline, Color.White);
            spriteBatch.DrawString(fontHUD, string.Format("Level: {0}", Resources.Level), levelTextPos, Color.Yellow);
            spriteBatch.DrawString(fontHUD, string.Format("{0} / {1}", Resources.XP, Resources.XPToNextLevel), xpTextPos, Color.Yellow);
            spriteBatch.DrawString(fontHUD, btnShopText, btnShopTextPos, Color.LightGreen);
            spriteBatch.DrawString(fontHUD, btnSkillText, btnSkillTextPos, Color.LightBlue);
            spriteBatch.DrawString(fontHUD, weaponText, weaponTextPos, Color.LimeGreen);
            spriteBatch.Draw(equipedWeaponIcon, weaponIconPos, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.9f);
            spriteBatch.End();
        }

        public void SetHealthBarPositions(float offsett)
        {
            healthbarWidth = barTex.Width;
            shieldbarWidth = barTex.Width;
            healthbarHeight = 40;
            healthbarSize = new Rectangle(healthbarHeight, window.ClientBounds.Height - healthbarHeight, barTex.Width, barTex.Height / 2);
            shieldbarSize = new Rectangle(healthbarHeight, window.ClientBounds.Height - healthbarHeight * 2, barTex.Width, barTex.Height / 2);

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
            levelTextPos = new Vector2(xpBarSize.X - fontHUD.MeasureString(levelText).X - offsett, xpBarSize.Y);
            xpText = string.Format("{0} / {1}", Resources.XP, Resources.XPToNextLevel);
            xpTextPos = new Vector2(xpBarSize.X + barTex.Width / 2 - fontHUD.MeasureString(xpText).X / 2, xpBarSize.Y + fontHUD.MeasureString(xpText).Y + offsett);
        }

        public void SetButtons()
        {
            btnSkillText = "Skills";
            btnShopText = "Shop";
            this.btnSkillUI = new UIButton(new Rectangle((int)(window.ClientBounds.Width - 60), (int)(window.ClientBounds.Height - 60), 50, 50), AssetManager.TextureAsset("button_blue"), OpenSkillUI);
            this.btnShopUI = new UIButton(new Rectangle((int)(btnSkillUI.Hitbox.X - 60), (int)(btnSkillUI.Hitbox.Y), 50, 50), AssetManager.TextureAsset("button_green"), OpenShopMenu);
            btnSkillUI.HoveredTexture = AssetManager.TextureAsset("button_red");
            btnShopUI.HoveredTexture = AssetManager.TextureAsset("button_red");
            btnSkillTextPos = new Vector2(btnSkillUI.Hitbox.X, btnSkillUI.Hitbox.Y - fontHUD.MeasureString(btnSkillText).Y);
            btnShopTextPos = new Vector2(btnShopUI.Hitbox.X, btnShopUI.Hitbox.Y - fontHUD.MeasureString(btnShopText).Y);
        }

        public void HealthChange(HealthChangeEvent e)
        {
            //Note for the % version of this part of the HUD. Replace the Entinty.Player.Max... with EntityManager.Player.TotalHealth
            healthbarSize.Width = (int)((EntityManager.Player.Health / EntityManager.Player.MaxHealth) * barTex.Width);
            shieldbarSize.Width = (int)((EntityManager.Player.Shields / EntityManager.Player.MaxShields) * barTex.Width);
        }

        public void XpChange(GainXpEvent e)
        {
            xpBarSize.Width = (int)((Resources.XP / Resources.XPToNextLevel) * barTex.Width);
        }
    }
}
