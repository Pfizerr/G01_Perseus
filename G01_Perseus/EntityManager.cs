using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace G01_Perseus
{
    public static class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();

        public static void CreatePlayer()
        {
            entities.Add(new Player(position: new Vector2(250, 250), color: Color.Blue, texture: AssetManager.GetTextureByAssetName("player_ship_asset"), scale: new Vector2(0.2f, 0.2f)));
        }

        public static void CreateEnemy()
        {
            entities.Add(new Enemy(new Vector2(200, 200), 3, Color.Red, new Point(15, 15)));
        }

        public static void AddBullet(Bullet bullet) => entities.Add(bullet);

        public static void Update(GameTime gameTime)
        {
            for(int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }
        }

        public static void HandleCollisions()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(spriteBatch, 0, 0, 0, 0, 0, 0);
            }
        }

    }
}