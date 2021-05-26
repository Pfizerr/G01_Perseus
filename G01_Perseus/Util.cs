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
                int y = px / width;
                bool leftOrRight = (x == 0) || (x == width - 1);
                bool topOrBottom = (y == 0) || (y == height - 1);

                //data[px] = (leftOrRight || topOrBottom) ? borderColor : filledColor;
                data[px] = (leftOrRight || topOrBottom) ? borderColor : Color.Transparent;
            }
            texture.SetData(data);
            return texture;
        }
               

        public static Texture2D CreateSpaceTexture(int seed, int width, int height, int starCount, int minSize, int maxSize)
        {
            Random random = new Random(seed);
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }

            for(int i = 0; i < starCount; i++)
            {
                int size = random.Next(minSize, maxSize);
                Point point = new Point(random.Next(0, width), random.Next(0, width));
                if(point.X-size < 0 || point.X+size >= width || point.Y-size < 0 || point.Y+size >= width)
                {
                    i--;
                    continue;
                }

                float diameter = size / 2f;
                float diameterSquared = diameter * diameter;

                for (int x = point.X-size; x < point.X + size; x++)
                {
                    for (int y = point.Y-size; y < point.Y + size; y++)
                    {
                        int index = x * width + (y);
                        Vector2 pos = new Vector2(point.X - x , point.Y - y);
                        if (pos.LengthSquared() <= diameterSquared && index >= 0 && index < width * height)
                        {
                            int alpha = (int)(((diameterSquared*100 + 1) / (pos.LengthSquared()*100 + 1)) * 100);
                            data[index] = new Color(255,255,255, data[index].A + alpha);
                        }
                    }
                }
            }


            texture.SetData(data);
            return texture;
        }

        public static Vector2 Clamp(Vector2 subject, Vector2 min, Vector2 max)
        {
            subject.X = subject.X < max.X ? subject.X : max.X;
            subject.X = subject.X > min.X ? subject.X : min.X;
            subject.Y = subject.Y < max.Y ? subject.Y : max.Y;
            subject.Y = subject.Y > min.Y ? subject.Y : min.Y;
            return subject;
        }

    }
}
