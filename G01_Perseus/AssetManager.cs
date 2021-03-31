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
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Texture2D> spriteSheets;

        public static void LoadContent(ContentManager content)
        {
            LoadTextureAsset("enemy_ship_asset", content.Load<Texture2D>(@"textures\EnemyShip"));
            LoadTextureAsset("player_ship_asset", content.Load<Texture2D>(@"textures\PlayerShip"));
            content.Load<Texture2D>(@"spritesheets\ProjectileSpritesheet"); //
        }

        public static void LoadTextureAsset(string assetName, Texture2D texture) => textures.Add(assetName, texture);

        public static void LoadFontAsset(string assetName, SpriteFont font) => fonts.Add(assetName, font);

        public static Texture2D GetTextureByAssetName(string assetName) => textures[assetName] == null ? null : textures[assetName];

        public static SpriteFont GetFontByAssetName(string assetName) => fonts[assetName] == null ? null : fonts[assetName];

    }
}
