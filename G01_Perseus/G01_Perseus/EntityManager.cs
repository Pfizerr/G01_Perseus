using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace G01_Perseus
{
    public static class EntityManager
    {
        public static List<Entity> entities;

        public static void CreatePlayer()
        {
            entities.Add(new Player(new Vector2(200, 200), 5f, Color.Blue, new Point(50, 50)));
        }

        public static void CreateEnemy()
        {

        }

        public static void Update(GameTime gameTime)
        {
            foreach(Entity entity in entities)
            {
                entity.Update(gameTime);
            }
        }

        public static void HandleCollisions()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}