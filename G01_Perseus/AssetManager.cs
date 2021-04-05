using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace G01_Perseus
{
    public static class AssetManager
    {
        public static Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>(); 
        public static Dictionary<string, SpriteFont> loadedFonts = new Dictionary<string, SpriteFont>();
        //public static Dictionary<string, Sprite> loadedSprites = new Dictionary<string Sprite>();
        //public static Dictionary<string, SpriteSheet> loadedSpriteSheets = new Dictionary<string, SpriteSheet>();

        public static void AddAsset(string assetName, Texture2D asset)
        {
            loadedTextures.Add(assetName, asset);
        }

        public static void AddAsset(string assetName, SpriteFont asset)
        {
            loadedFonts.Add(assetName, asset);
        }

        public static Texture2D TextureAsset(string assetName)
        {
            return loadedTextures[assetName];
        }

        public static SpriteFont FontAsset(string assetName)
        {
            return loadedFonts[assetName];
        }

        public static void LoadAssets(ContentManager content)
        {
            loadedTextures = new Dictionary<string, Texture2D>();
            loadedFonts = new Dictionary<string, SpriteFont>();

            //Textures
            loadedTextures.Add("player_ship", content.Load<Texture2D>(@"textures\PlayerShip2"));
            loadedTextures.Add("enemy_ship", content.Load<Texture2D>(@"textures\EShip"));
            loadedTextures.Add("enemy_ship2", content.Load<Texture2D>(@"textures\EnemyShip"));
            loadedTextures.Add("projectile_green", content.Load<Texture2D>(@"textures\projektileGreen"));
            loadedTextures.Add("projectile_pink", content.Load<Texture2D>(@"textures\projektilePink"));
            loadedTextures.Add("projectile_blue", content.Load<Texture2D>(@"textures\projektileBlue"));
            loadedTextures.Add("projectile_yellow", content.Load<Texture2D>(@"textures\projektileYellow")); 
            loadedTextures.Add("beam", content.Load<Texture2D>(@"textures\Beam"));
            loadedTextures.Add("planet_1", content.Load<Texture2D>(@"textures\planet1"));
            loadedTextures.Add("planet_2", content.Load<Texture2D>(@"textures\planet2"));
            loadedTextures.Add("planet_3", content.Load<Texture2D>(@"textures\planet3"));
            loadedTextures.Add("resource_currency", content.Load<Texture2D>(@"textures\Currency"));
            //Fonts
            loadedFonts.Add("default_font", content.Load<SpriteFont>(@"fonts\default_font"));
            //Spritesheets

        }
    }
}
