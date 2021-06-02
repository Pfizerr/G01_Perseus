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
    public class SkillInterface : GameState
    {        
        private Texture2D backgroundTex, skillbarOutlineTex, buttonTex, buttonHoveredTex;
        private Texture2D[] skillIcons;

        private UIButton exit, addDamage, addShields, addHealth, addFireRate, resetSP;
        private UIButton[] buttonsArray;
        private List<Rectangle> skillBarPos, skillBarPosOutline, skillIconPos;
        private Rectangle bounds;
        private SpriteFont fontSkillUI;
        //private Action[] actions; //This is just for an automated system of the "add" buttons. It is not used currently
        private float skillBarMaxWidth;
        private string btnExitText, btnResetText, skillPointsText;
        private Vector2 btnExitTextPos, btnResetTextPos, skillPointsTextPos;        

        public SkillInterface(GameWindow window)
        {
            this.fontSkillUI = AssetManager.FontAsset("default_font");
            this.Transparent = true;
            this.buttonTex = AssetManager.TextureAsset("skill_button_blue");
            this.buttonHoveredTex = AssetManager.TextureAsset("skill_button_red");
            this.backgroundTex = AssetManager.TextureAsset("skill_UI");
            this.skillIcons = new Texture2D[] { AssetManager.TextureAsset("damage_icon"), AssetManager.TextureAsset("health_icon"), AssetManager.TextureAsset("shield_icon"), AssetManager.TextureAsset("fire_rate_icon") };
            this.bounds = new Rectangle(window.ClientBounds.Width / 2 - (int)(backgroundTex.Width * 1.7f / 2), window.ClientBounds.Height / 2 - (int)(backgroundTex.Height * 1.7f / 2), (int)(backgroundTex.Width * 1.7f), (int)(backgroundTex.Height * 1.7f));
            CreateUiButtons();

            SetIconsAndSkillBars();

            btnExitText = "Exit";
            btnResetText = "Reset skill points";
            skillPointsText = string.Format("Skill Points: {0}", Resources.SkillPoints);
            btnExitTextPos = new Vector2(exit.Hitbox.X + exit.Hitbox.Width + 5, exit.Hitbox.Y);
            btnResetTextPos = new Vector2(resetSP.Hitbox.X + resetSP.Hitbox.Width + 5, resetSP.Hitbox.Y + resetSP.Hitbox.Height / 2 - fontSkillUI.MeasureString(btnResetText).Y / 2);
            skillPointsTextPos = new Vector2(btnResetTextPos.X - 250, btnResetTextPos.Y);
        }        

        public override void Update(GameTime gameTime)
        {
            exit.Update(gameTime);
            resetSP.Update(gameTime);
            foreach(UIButton button in buttonsArray)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, bounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            exit.Draw(spriteBatch, gameTime);
            resetSP.Draw(spriteBatch, gameTime);
            foreach(UIButton button in buttonsArray)
            {
                button.Draw(spriteBatch, gameTime);
            }

            for(int i = 0; i < skillIcons.Length; i++)
            {
                spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), skillBarPos[i], Color.Green);
                //spriteBatch.DrawString(AssetManager.FontAsset("default_font"), statLables[i], labelPosition[i], Color.Black);
                spriteBatch.Draw(skillbarOutlineTex, skillBarPosOutline[i], Color.White);
                spriteBatch.Draw(skillIcons[i], skillIconPos[i], Color.White);
            }

            spriteBatch.DrawString(fontSkillUI, btnExitText, btnExitTextPos, Color.Red);
            spriteBatch.DrawString(fontSkillUI, btnResetText, btnResetTextPos, Color.Red);
            spriteBatch.DrawString(fontSkillUI, string.Format("Skill points: {0}", Resources.SkillPoints), skillPointsTextPos, Color.Yellow);
            spriteBatch.End();
        }

        public void ExitUI()
        {
            EntityManager.Player.UpdateWeapons();
            EventManager.Dispatch(new PopStateEvent());
        }

        public void ResetSP()
        {
            Resources.ResetSkillpoints();
            for (int i = 0; i < skillBarPos.Count; i++)
            {
                CalculateBarWidth(i, 0); //This here is not good. Should use a reference with all the different skill points used
            }
        }

        public void IncreaseDamage()
        {
            Resources.AddSpDamage();
            CalculateBarWidth(0, Resources.SpDamage);
        }

        public void IncreaseHealth()
        {
            Resources.AddSpHealth();
            CalculateBarWidth(1, Resources.SpHealth);
        }

        public void IncreaseShields()
        {
            Resources.AddSpShields();
            CalculateBarWidth(2, Resources.SpShields);
        }

        public void IncreaseFireRate()
        {
            Resources.AddSpFireRate();
            CalculateBarWidth(3, Resources.SpFireRate);
        }

        public void CalculateBarWidth(int pos, float spUsed)
        {
            int newWidth = (int)(skillBarMaxWidth * (spUsed / Resources.MaxPoints));
            skillBarPos[pos] = new Rectangle(skillBarPos[pos].X, skillBarPos[pos].Y, newWidth, skillBarPos[pos].Height);
        }

        public void CreateUiButtons()
        {
            //Buttons
            this.exit = new UIButton(new Rectangle(bounds.X + 40, bounds.Y + 50, 20, 20), buttonTex, ExitUI);
            exit.HoveredTexture = buttonHoveredTex;
            this.resetSP = new UIButton(new Rectangle(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height - 70, 40, 40), buttonTex, ResetSP);
            resetSP.HoveredTexture = buttonHoveredTex;

            int offset = 60;
            this.addDamage = new UIButton(new Rectangle(bounds.X + bounds.Width - 80, bounds.Y + 20 + offset, 50, 50), buttonTex, IncreaseDamage);
            this.addHealth = new UIButton(new Rectangle(bounds.X + bounds.Width - 80, bounds.Y + 20 + offset * 2, 50, 50), buttonTex, IncreaseHealth);
            this.addShields = new UIButton(new Rectangle(bounds.X + bounds.Width - 80, bounds.Y + 20 + offset * 3, 50, 50), buttonTex, IncreaseShields);
            this.addFireRate = new UIButton(new Rectangle(bounds.X + bounds.Width - 80, bounds.Y + 20 + offset * 4, 50, 50), buttonTex, IncreaseFireRate);
            buttonsArray = new UIButton[] { addDamage, addHealth, addShields, addFireRate };

            foreach(UIButton button in buttonsArray)
            {
                button.HoveredTexture = buttonHoveredTex;
            }
        }

        public void SetIconsAndSkillBars()
        {
            //Skillbars and outlines
            skillBarPos = new List<Rectangle>();
            skillBarPosOutline = new List<Rectangle>();
            skillIconPos = new List<Rectangle>();

            skillBarMaxWidth = buttonsArray[0].Hitbox.X - (bounds.X + 120);
            for (int i = 0; i < buttonsArray.Length; i++)
            {
                skillBarPos.Add(new Rectangle(bounds.X + 100, buttonsArray[i].Hitbox.Y, (int)(skillBarMaxWidth * (Resources.SpDamage / Resources.MaxPoints)), buttonsArray[i].Hitbox.Height));
                skillBarPosOutline.Add(new Rectangle(skillBarPos[i].X, skillBarPos[i].Y, (int)(skillBarMaxWidth), skillBarPos[i].Height));
                skillIconPos.Add(new Rectangle(skillBarPos[i].X - buttonsArray[i].Hitbox.Width - 5, skillBarPos[i].Y, 50, 50));
            }

            skillbarOutlineTex = Util.CreateRectangleTexture((int)skillBarMaxWidth, buttonsArray[0].Hitbox.Height, Color.Black, Color.Transparent);
        }
    }
}
