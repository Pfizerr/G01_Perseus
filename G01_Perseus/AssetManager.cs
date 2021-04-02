using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public static class AssetManager
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void AddFont(string name, SpriteFont font)
        {
            fonts.Add(name, font);
        }

        public static void AddTexture(string name, Texture2D texture)
        {
            textures.Add(name, texture);
        }

        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public static SpriteFont GetFont(string name)
        {
            return fonts[name];
        }

        public static void LoadAssets(ContentManager content)
        {
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();

            textures.Add("player_ship_texture", content.Load<Texture2D>(@"Textures\player_ship"));
            textures.Add("enemy_ship_texture", content.Load<Texture2D>(@"Textures\enemy_ship"));
            fonts.Add("default_font", content.Load<SpriteFont>(@"Fonts\default_font"));
        }
    }
}
