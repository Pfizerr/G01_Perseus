using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Level
    {

        private int width;
        private int height;
        private int tileWidth;
        private int tileHeight;

        private SpriteFont font;

        private Background background;

        // This code is only for debugging purpose
        #region
        private Texture2D tileTexture;
        #endregion

        public Level(int width, int height, int tileWidth, int tileHeight, SpriteFont font)
        {
            this.width = width;
            this.height = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            this.font = font;

            background = new Background();
            
            this.tileTexture = Util.CreateRectangleTexture(tileWidth, tileHeight, Color.Cyan, Color.Transparent);    
        }
        
        public Point GetSectorCoordinates(int x, int y)
        {
            int ix = x % width >= 0 ? x % width : (x % width) + width;
            int iy = y % height >= 0 ? y % height : (y % height) + height;
            return new Point(ix, iy);
        }

        private string GetSectorString(int x, int y)
        {
            char c = Convert.ToChar('A' + x);
            string n = "" + y;
            return c + n;
        }
        
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {

            int tilesPerCameraX = (int)Math.Ceiling(camera.Viewport.Width / (float)tileWidth);
            int tilesPerCameraY = (int)Math.Ceiling(camera.Viewport.Height / (float)tileHeight);

            int startX = (int)Math.Floor((camera.CenterPosition.X - (tilesPerCameraX * tileWidth * 0.5f)) / tileWidth);
            int startY = (int)Math.Floor((camera.CenterPosition.Y - (tilesPerCameraY * tileHeight * 0.5f)) / tileHeight);
            int endX = startX + (int)(tilesPerCameraX) + 1;
            int endY = startY + (int)(tilesPerCameraY) + 1;
                       

            // Drawing of sectors
            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    background.Draw(spriteBatch, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    spriteBatch.Draw(tileTexture, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.4f);

                    Point sectorCoords = GetSectorCoordinates(x, y);
                    string text = GetSectorString(sectorCoords.X, sectorCoords.Y);
                    Vector2 textDim = font.MeasureString(text);
                    spriteBatch.DrawString(font, text, new Vector2(x * tileWidth + 5, y * tileHeight + 5), Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.4f);
                    spriteBatch.DrawString(font, text, new Vector2((x * tileWidth) + tileWidth - textDim.X - 5, y * tileHeight + 5), Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.4f);
                    spriteBatch.DrawString(font, text, new Vector2((x * tileWidth) + tileWidth - textDim.X - 5, (y * tileHeight) + tileHeight - textDim.Y), Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.4f);
                    spriteBatch.DrawString(font, text, new Vector2(x * tileWidth + 5, (y * tileHeight) + tileHeight - textDim.Y), Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.4f);
                }
            }

        }
    }
}
