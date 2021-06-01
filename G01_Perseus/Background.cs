using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    class Background
    {
        private Texture2D[] tex;

        public Background(int tileWidth, int tileHeight, int multiplier = 1)
        {
            this.tex = new Texture2D[3];
            tex[0] = Util.CreateSpaceTexture(0, tileWidth, tileHeight, 100 * multiplier, 10,15);
            tex[1] = Util.CreateSpaceTexture(0, tileWidth, tileHeight, 1000 * multiplier, 1, 5);
            tex[2] = Util.CreateSpaceTexture(1, tileWidth, tileHeight, 100 * multiplier, 5, 7);

        }

        public void Draw(SpriteBatch sb, Rectangle bounds, Vector2 cameraPosition)
        {
            sb.Draw(tex[0], new Vector2(bounds.X, bounds.Y) - cameraPosition * 0.0001f, Color.White);
            sb.Draw(tex[1], new Vector2(bounds.X, bounds.Y) - cameraPosition * 0.05f, Color.White);
            sb.Draw(tex[2], new Vector2(bounds.X, bounds.Y) - cameraPosition * 0.1f, Color.White);

        }

    }
}
