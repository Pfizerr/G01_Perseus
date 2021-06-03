using G01_Perseus.EventSystem.Listeners;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class InGameState : GameState, GameOverListener
    {

        private static Camera camera;
        private Level level;

        public InGameState(Camera camera)
        {
            InGameState.camera = camera;

            int tileWidth = 1180;
            int tileHeight = 1080;
            level = new Level(10, 10, tileWidth, tileHeight, AssetManager.FontAsset("sector_font"));

            

            InitGame();
        }

        public static void InitGame()
        {
            EntityManager.CreateEnemySpawner(new Vector2(200, 200), EnemySpawner.Type.Raptor, 10000);
            camera.FollowTarget = EntityManager.Player;
        }

        public override void Update(GameTime gameTime)
        {
            EntityManager.Update(gameTime);
            camera.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, camera.Translation);
            level.Draw(spriteBatch, camera);
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void OnRespawnPlayer()
        {
            EntityManager.CreatePlayer();
        }
    }
}
