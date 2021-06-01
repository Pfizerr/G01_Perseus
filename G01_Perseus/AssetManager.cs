using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace G01_Perseus
{
    
    public static class AssetManager
    {
        /// <summary>
        /// This dictionary contains all the textures that have been loaded into the game, these are accessible with the associated string. Call loadedTextures["texture_name"] to get a texture.
        /// </summary>
        public static Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>(); 

        /// <summary>
        /// This dictionary contains all the fonts that have been loaded into the game, these are accessible with the associated string. Call loadedFonts ["font_name"] to get a font.
        /// </summary>
        public static Dictionary<string, SpriteFont> loadedFonts = new Dictionary<string, SpriteFont>();

        /// <summary>
        ///  Add a new asset to the manager, can be textures or fonts.
        /// </summary>
        /// <param name="assetName">The name by which you will be able to retrieve the specified asset.</param>
        /// <param name="asset">The asset to be added.</param>
        public static void AddAsset(string assetName, Texture2D asset) { loadedTextures.Add(assetName, asset); }
      
        /// <summary>
        /// Add a new asset to the manager, can be textures or fonts.
        /// </summary>
        /// <param name="assetName">The name by which you will be able to retrieve the specified asset.</param>
        /// <param name="asset">The asset to be added.</param>
        public static void AddAsset(string assetName, SpriteFont asset) { loadedFonts.Add(assetName, asset); }
 
        /// <summary>
        /// Try to find a loaded texture asset by a given assetName.
        /// </summary>
        /// <param name="assetName">The name of the asset, this should be the assetName you specified during the loading of the assets.</param>
        /// <returns>An associated texture asset.</returns>
        public static Texture2D TextureAsset(string assetName) => loadedTextures[assetName];

        /// <summary>
        /// Try to find a loaded texture asset by a given assetName, then return a new sprite instantiated with that texture
        /// </summary>
        /// <param name="assetName">The name of the asset, this should be the assetName you specified during the loading of the assets.</param>
        /// <returns>An associated texture asset.</param>
        /// <returns></returns>
        public static Sprite SpriteAsset(string assetName) => loadedTextures[assetName] != null ? new Sprite(loadedTextures[assetName]) : null;

        /// <summary>
        /// Try to find a loaded font asset by a given assetName.
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static SpriteFont FontAsset(string assetName) => loadedFonts[assetName];

        /// <summary>
        /// These assets are loaded at game start.
        /// </summary>
        public static void LoadAssets(ContentManager content)
        {
            loadedTextures = new Dictionary<string, Texture2D>();
            loadedFonts = new Dictionary<string, SpriteFont>();

            //Textures
            loadedTextures.Add("planet_highlight_outline2_red", content.Load<Texture2D>(@"textures\Planet_highlighted_outline2_red"));
            loadedTextures.Add("planet_highlight_outline2_orange", content.Load<Texture2D>(@"textures\Planet_highlighted_outline2_orange"));
            loadedTextures.Add("planet_highlight_outline2_blue", content.Load<Texture2D>(@"textures\Planet_highlighted_outline2_blue"));
            loadedTextures.Add("planet_highlight_outline2_green", content.Load<Texture2D>(@"textures\Planet_highlighted_outline2_green"));
            loadedTextures.Add("player_ship", content.Load<Texture2D>(@"textures\PlayerShip2"));
            loadedTextures.Add("enemy_ship", content.Load<Texture2D>(@"textures\EShip"));
            loadedTextures.Add("enemy_ship2", content.Load<Texture2D>(@"textures\EnemyShip"));
            loadedTextures.Add("enemy_ship3", content.Load<Texture2D>(@"textures\EnemyShip3"));
            loadedTextures.Add("projectile_green", content.Load<Texture2D>(@"textures\projektileGreen"));
            loadedTextures.Add("projectile_pink", content.Load<Texture2D>(@"textures\projektilePink"));
            loadedTextures.Add("projectile_blue", content.Load<Texture2D>(@"textures\projektileBlue"));
            loadedTextures.Add("projectile_yellow", content.Load<Texture2D>(@"textures\projektileYellow"));
            loadedTextures.Add("beam", content.Load<Texture2D>(@"textures\Beam"));
            loadedTextures.Add("planet1", content.Load<Texture2D>(@"textures\planet1"));
            loadedTextures.Add("planet2", content.Load<Texture2D>(@"textures\planet2"));
            loadedTextures.Add("planet3", content.Load<Texture2D>(@"textures\planet3"));
            loadedTextures.Add("resource_currency", content.Load<Texture2D>(@"textures\Currency"));
            loadedTextures.Add("gradient_bar", content.Load<Texture2D>(@"textures\GradientBar"));
            loadedTextures.Add("skill_UI", content.Load<Texture2D>(@"textures\SkillMonitorBackground"));
            loadedTextures.Add("space_background", content.Load<Texture2D>(@"textures\Space"));
            loadedTextures.Add("sun", content.Load<Texture2D>(@"textures\Sun"));
            loadedTextures.Add("smokeParticle_sprite", content.Load<Texture2D>(@"textures\SmokeSpriteSheet"));
            loadedTextures.Add("button_red", content.Load<Texture2D>(@"textures\ButtonRed"));
            loadedTextures.Add("button_blue", content.Load<Texture2D>(@"textures\ButtonBlue"));
            loadedTextures.Add("monitor_focused", content.Load<Texture2D>(@"textures\MonitorFocused"));
            loadedTextures.Add("monitor_not_focused", content.Load<Texture2D>(@"textures\MonitorNotFocused"));
            loadedTextures.Add("button_background_available", content.Load <Texture2D>(@"textures\ButtonBackgroundAvailable"));
            loadedTextures.Add("button_background_unavailable", content.Load <Texture2D>(@"textures\ButtonBackgroundUnavailable"));
            loadedTextures.Add("button_background_hovered", content.Load<Texture2D>(@"textures\ButtonBackgroundHovered"));
            loadedTextures.Add("button_background_available_transparent", content.Load<Texture2D>(@"textures\ButtonBackgroundAvailableTransparent"));
            loadedTextures.Add("button_background_unavailable_transparent", content.Load<Texture2D>(@"textures\ButtonBackgroundUnavailableTransparent"));
            loadedTextures.Add("button_background_hovered_transparent", content.Load<Texture2D>(@"textures\ButtonBackgroundHoveredTransparent"));

            loadedTextures.Add("button_green", content.Load<Texture2D>(@"textures\ButtonGreen"));
            loadedTextures.Add("damage_icon", content.Load<Texture2D>(@"textures\DamageIconSmall"));
            loadedTextures.Add("health_icon", content.Load<Texture2D>(@"textures\HeartIconSmall"));
            loadedTextures.Add("shield_icon", content.Load<Texture2D>(@"textures\ShieldIconSmall"));
            loadedTextures.Add("fire_rate_icon", content.Load<Texture2D>(@"textures\TimerIconSmall"));
            loadedTextures.Add("skill_button_blue", content.Load<Texture2D>(@"textures\SkillButtonBlue"));
            loadedTextures.Add("skill_button_red", content.Load<Texture2D>(@"textures\SkillButtonRed"));
            loadedTextures.Add("start_menu_background_UI", content.Load<Texture2D>(@"textures\StartMenuBackground"));
            loadedTextures.Add("journal_menu_background", content.Load<Texture2D>(@"textures\JournalMenuBackground"));

            loadedTextures.Add("tripple_shot_icon", content.Load<Texture2D>(@"textures\TrippleShootIcon"));
            loadedTextures.Add("shop_menu_background_UI", content.Load<Texture2D>(@"textures\ShopMenuBackground"));
            loadedTextures.Add("quest_menu_background_UI", content.Load<Texture2D>(@"textures\QuestMenuBackground"));

            //Fonts
            loadedFonts.Add("default_font", content.Load<SpriteFont>(@"fonts\default_font"));
            loadedFonts.Add("default_font_small", content.Load<SpriteFont>(@"fonts\default_font_small"));
            loadedFonts.Add("sector_font", content.Load<SpriteFont>(@"fonts\sector_font"));
            loadedFonts.Add("main_menu_font", content.Load<SpriteFont>(@"fonts\main_menu_font"));
            //Sprite sheets

        }
    }
}
