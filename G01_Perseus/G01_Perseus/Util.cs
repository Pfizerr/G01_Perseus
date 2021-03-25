using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public static class Util
    {
        private static GraphicsDevice device;
        public static GraphicsDevice Device { private get => Util.device; set => Util.device = value; }

        public static Texture2D CreateFilledRectangleTexture(Color pxColor, int pxWidth, int pxHeight)
        {
            Texture2D texture = new Texture2D(device, pxWidth, pxHeight);
            Color[] data = new Color[pxWidth * pxHeight];
            for (int px = 0; px < data.Length; px++)
            {
                data[px] = pxColor;
            }
            texture.SetData(data);
            return texture;
        }

        public static Texture2D CreateRectangleTexture(int width, int height, Color borderColor, Color filledColor)
        {
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];
            for (int px = 0; px < data.Length; px++)
            {
                int x = px % width;
                int y = px / height;
                bool leftOrRight = (x == 0) || (x == width - 1);
                bool topOrBottom = (y == 0) || (y == height - 1);

                data[px] = (leftOrRight || topOrBottom) ? borderColor : filledColor;
            }
            texture.SetData(data);
            return texture;
        }
    }
}
