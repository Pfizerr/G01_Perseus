using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Sprite
    {

        private Point start;
        private Point spriteSize;

        private int xIndex;
        private int yIndex;
        private int columns;
        private int rows;
        private int offset;

        private Texture2D texture;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.start = Point.Zero;
            this.spriteSize = new Point(texture.Width, texture.Height);

            this.xIndex = 0;
            this.yIndex = 0;
            this.columns = 1;
            this.rows = 1;
        }

        public Sprite(Texture2D texture, Point start, Point spriteSize)
        {
            this.texture = texture;
            this.start = start;
            this.spriteSize = spriteSize;

            this.columns = texture.Width / spriteSize.X;
            this.rows = texture.Height / spriteSize.Y;
            this.XIndex = start.X / spriteSize.X;
            this.YIndex = start.Y / spriteSize.Y;
        }

        public int XIndex
        {
            get { return xIndex; }
            set { this.xIndex = value % columns; }
        }

        public int YIndex
        {
            get { return yIndex; }
            set { this.yIndex = value % rows; }
        }

        public int Width
        {
            get { return spriteSize.X; }
            private set { spriteSize.X = value; }
        }

        public int Height
        {
            get { return spriteSize.Y; }
            private set { spriteSize.Y = value; }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, Vector2 origin, float rotation, float layerDepth)
        {
            Point spriteStart = new Point(start.X + (spriteSize.X * XIndex) + (offset * XIndex), start.Y + (spriteSize.Y * YIndex) + (offset * YIndex));
            Rectangle sourceRectangle = new Rectangle(spriteStart, spriteSize);
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }


        public virtual void Draw(SpriteBatch spriteBatch, Rectangle hitbox, Rectangle? source, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth)
        {
            Point spriteStart = new Point(start.X + (spriteSize.X * XIndex) + (offset * XIndex), start.Y + (spriteSize.Y * YIndex) + (offset * YIndex));
            Rectangle sourceRectangle = new Rectangle(spriteStart, spriteSize);
            spriteBatch.Draw(texture, hitbox, sourceRectangle, color, rotation, origin, spriteEffects, layerDepth);
        }
    }
}
