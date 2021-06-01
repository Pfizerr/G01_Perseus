using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    public class UIButton
    {

        public delegate void OnClick();

        public UIButton(Rectangle rectangle, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Action = onClick;

            this.Opacity = 1.0f;

            //DEBUG 
            Texture = Util.CreateRectangleTexture(rectangle.Width, rectangle.Height, Color.Black, Color.Transparent);
            HoveredTexture = Util.CreateRectangleTexture(rectangle.Width, rectangle.Height, Color.Black, Color.Cyan);
        }

        public UIButton(Rectangle rectangle, Texture2D texture, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Texture = texture;
            this.Action = onClick;

            this.Opacity = 1.0f;
        }

        public UIButton(Rectangle rectangle, Texture2D texture, string text, SpriteFont font, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Texture = texture;
            this.Text = text;
            this.Action = onClick;
            this.Font = font;

            this.TextLocation = new Vector2(rectangle.Center.X, rectangle.Center.Y);
            this.Opacity = 1.0f;
        }

        public UIButton(Rectangle rectangle, Texture2D texture, string text, SpriteFont font, Vector2 textPositionOffset, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Texture = texture;
            this.Text = text;
            this.Action = onClick;
            this.Font = font;

            this.Font = AssetManager.FontAsset("default_font");
            this.TextLocation = new Vector2(rectangle.Center.X + textPositionOffset.X, rectangle.Center.Y + textPositionOffset.Y);
            this.Opacity = 1.0f;
        }

        public Rectangle Hitbox
        {
            get;
            private set;
        }

        public Texture2D Texture
        {
            get;
            set;
        }

        public Texture2D HoveredTexture
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public OnClick Action
        {
            get;
            set;
        }

        public SpriteFont Font
        {
            get;
            set;
        }

        public Vector2 TextLocation
        {
            get;
            set;
        }

        public bool IsMouseHovered
        {
            get;
            set;
        }

        public float Opacity
        {
            get;
            set;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = KeyMouseReader.MouseScreenPosition;            
            IsMouseHovered = Hitbox.Contains(mousePosition.ToPoint());

            if(IsMouseHovered && KeyMouseReader.LeftClick() && Action != null && KeyMouseReader.oldMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed) 
            {
                Action();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(Texture != null)
            {
                if(IsMouseHovered && HoveredTexture != null)
                {
                    spriteBatch.Draw(HoveredTexture, Hitbox, null, Color.White * Opacity, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
                else
                {
                    spriteBatch.Draw(Texture, Hitbox, null, Color.White * Opacity, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
            }

            if(Text != null)
            {
                Vector2 textSize = Font.MeasureString(Text);
                spriteBatch.DrawString(Font, Text, new Vector2(TextLocation.X - textSize.X / 2, TextLocation.Y - textSize.Y / 2), Color.White);
            }
        }

    }
}
