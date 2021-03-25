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

        public static float CalculateAngle(Vector2 position, Vector2 targetPosition)
        {
            Vector2 dPos = position - targetPosition;
            return (float)Math.Atan2(dPos.X, dPos.Y);
        }

        public static Texture2D CreateTexture(Color pxColor, int pxWidth, int pxHeight)
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
    }
}
