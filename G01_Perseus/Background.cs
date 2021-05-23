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
        private List<Vector2> front, middle, back;
        private int frontSpace, middleSpace, backSpace;
        private float frontSpeed, middleSpeed, backSpeed;
        private Texture2D[] tex;
        private GameWindow window;      

        public Background()
        {
            this.tex = new Texture2D[3];
            tex[0] = AssetManager.TextureAsset("space_background");
            tex[1] = AssetManager.TextureAsset("space_background");
            tex[2] = AssetManager.TextureAsset("space_background");

            front = new List<Vector2>();
            frontSpace = tex[0].Width / 5;
            frontSpeed = 0.75f;

            back = new List<Vector2>();
            back.Add(new Vector2(0));
            backSpeed = 0.25f;
        }

        public void Draw(SpriteBatch sb, Rectangle bounds)
        {           
            sb.Draw(tex[0], bounds, Color.White);                  
        }

    }
}
