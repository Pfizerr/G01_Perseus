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

        private bool isMouseHovered;

        public UIButton(Rectangle rectangle, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Action = onClick;

            //DEBUG 
            Texture = Util.CreateRectangleTexture(rectangle.Width, rectangle.Height, Color.Black, Color.Transparent);
            HoveredTexture = Util.CreateRectangleTexture(rectangle.Width, rectangle.Height, Color.Black, Color.Cyan);
        }

        public UIButton(Rectangle rectangle, Texture2D texture, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Texture = texture;
            this.Action = onClick;
        }

        public UIButton(Rectangle rectangle, Texture2D texture, string text, OnClick onClick = null)
        {
            this.Hitbox = rectangle;
            this.Texture = texture;
            this.Text = text;
            this.Action = onClick;
        }

        public Rectangle Hitbox
        {
            get;
            private set;
        }

        public Texture2D Texture
        {
            get;
            private set;
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

        public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = KeyMouseReader.MouseScreenPosition;            
            isMouseHovered = Hitbox.Contains(mousePosition.ToPoint());

            if(isMouseHovered && KeyMouseReader.LeftClick() && Action != null)
            {
                Action();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(Texture != null)
            {
                if(isMouseHovered)
                {
                    spriteBatch.Draw(HoveredTexture, Hitbox, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
                else
                {
                    spriteBatch.Draw(Texture, Hitbox, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
            }

            if(Text != null)
            {
                //DRAW TEXT
            }
        }

    }
}
