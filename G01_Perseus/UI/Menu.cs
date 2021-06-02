using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Menu
    {
        private Texture2D debugTexture;


        private SpriteFont font;
        private MenuOption hoveredOption;

        public Menu(Rectangle bounds, SpriteFont font)
        {
            this.Bounds = bounds;
            this.font = font;
            this.Options = new List<MenuOption>();
            this.debugTexture = Util.CreateRectangleTexture(600, 100, Color.White, Color.Transparent);
        }
        
        public Rectangle Bounds
        {
            get;
            private set;
        }

        public List<MenuOption> Options
        {
            get;
            private set;
        }

        private Rectangle GetBounds(int option)
        {
            int width = Bounds.Width;
            int height = Bounds.Height / Options.Count;
            int x = Bounds.X;
            int y = Bounds.Y + option * height;
            return new Rectangle(x, y, width, height);
        }

        public void Update(GameTime gameTime)
        {
            hoveredOption = null;

            for(int i = 0; i < Options.Count; i++)
            {
                Rectangle rect = GetBounds(i);
                if(rect.Contains(KeyMouseReader.MouseScreenPosition))
                {
                    hoveredOption = Options[i];
                    if(KeyMouseReader.LeftClick())
                    {
                        Options[i].Action();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            for(int i = 0; i < Options.Count; i++)
            {
                MenuOption option = Options[i];
                Rectangle rect = GetBounds(i);

                Color color = hoveredOption == option ? Color.White : Color.Gray;
                if(option.Text == null)
                {
                    spriteBatch.Draw(option.Texture, rect, Color.White);
                }
                else if(option.Texture == null)
                {
                    Vector2 textDim = font.MeasureString(option.Text);
                    Vector2 textPosition = new Vector2(rect.X + (rect.Width / 2) - (textDim.X / 2), rect.Y + (rect.Height / 2) - (textDim.Y / 2));
                    spriteBatch.DrawString(font, option.Text, textPosition, color);
                }
                else
                {
                    Vector2 textDim = font.MeasureString(option.Text);
                    Vector2 textPosition = new Vector2(rect.X + (rect.Width / 2) - (textDim.X / 2), rect.Y + (rect.Height / 2) - (textDim.Y / 2));
                    Rectangle iconPosition = new Rectangle(rect.X - rect.Height - 20, (int)textPosition.Y - 5, rect.Height - 10, rect.Height - 10);
                    spriteBatch.DrawString(font, option.Text, textPosition, color);
                    spriteBatch.Draw(option.Texture, iconPosition, Color.White);
                }

                spriteBatch.Draw(debugTexture, rect, color);
            }
        }
    }

    public class MenuOption
    {
        public delegate void internalAction();

        public MenuOption(string text, internalAction action)
        {
            this.Text = text;
            this.Action = action;
        }

        public MenuOption(Texture2D texture, internalAction action)
        {
            this.Texture = texture;
            this.Action = action;
        }

        public MenuOption(Texture2D texture, string text, internalAction action)
        {
            this.Texture = texture;
            this.Text = text;
            this.Action = action;
        }

        public string Text
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get;
        }

        public internalAction Action
        {
            get;
        }
    }
}
