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
        List<Vector2> front, middle, back;
        int frontSpace, middleSpace, backSpace;
        float frontSpeed, middleSpeed, backSpeed;
        Texture2D[] tex;
        GameWindow window;        

        public Background()
        {
            this.tex = new Texture2D[3];
            //this.window = window;
            //tex[0] = Content.Load<Texture2D>("space");
            //tex[1] = Content.Load<Texture2D>("space");
            //tex[2] = Content.Load<Texture2D>("space");
            tex[0] = AssetManager.TextureAsset("space_background");
            tex[1] = AssetManager.TextureAsset("space_background");
            tex[2] = AssetManager.TextureAsset("space_background");

            front = new List<Vector2>();
            frontSpace = tex[0].Width / 5;
            frontSpeed = 0.75f;

            //for(int i = 0; i < (window.ClientBounds.Width / middleSpace) + 2; i++)
            //{
            //    middle.Add(new Vector2(i * middleSpace, window.ClientBounds.Height - tex[0].Height - tex[1].Height));
            //}

            back = new List<Vector2>();
            back.Add(new Vector2(0));
            //backSpace = window.ClientBounds.Width / 3;
            backSpeed = 0.25f;

            //for (int i = 0; i < (window.ClientBounds.Width / backSpace) + 2; i++)
            //{
            //    back.Add(new Vector2(i * backSpace, window.ClientBounds.Height - tex[0].Height - (int)(tex[1].Height * 1.5)));
            //}
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb, Rectangle bounds)
        {
            
             sb.Draw(tex[0], bounds, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            
            //foreach (Vector2 v in middle)
            //{
            //    sb.Draw(tex[1], v, Color.White);
            //}
            //foreach (Vector2 v in front)
            //{
            //    sb.Draw(tex[0], v, Color.White);
            //}
        }

    }
}
