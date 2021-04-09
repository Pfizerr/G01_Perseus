using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus
{
    class TextureManager
    {
        public static Texture2D GradientBar;

        public static void LoadTextures(ContentManager content)
        {
            GradientBar = content.Load<Texture2D>("GradientBar");

        }
    }
}
