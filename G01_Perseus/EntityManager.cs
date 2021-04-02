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
            entities.Add(new Player(new Vector2(250, 250), 3, Color.Blue, new Vector2(0.2f, 0.2f), 100));
        }

        public static void CreateEnemy()
        {
            entities.Add(new Enemy(new Vector2(200, 200), 3, Color.Red, new Vector2(15, 15), 100, 25, true));
        }

        public static void AddBullet(Bullet bullet) => entities.Add(bullet);

        public static void Update(GameTime gameTime)
        {
            for(int i = 0; i < entities.Count; i++)
            {
                if(!entities[i].IsAlive)
                {
                    entities.RemoveAt(i);
                    continue;
                }

                entities[i].Update(gameTime);

                for (int y = 1; y < entities.Count; y++)
                {
                    if (entities[i] == entities[y])
                    {
                        continue;
                    }
                    if (entities[i].HitBox.Intersects(entities[y].HitBox))
                    {
                        // Collision Event
                        Console.WriteLine(entities[i].ToString() + " collided with " + entities[y].ToString());
                        HandleCollisions(entities[i], entities[y]);
                    }
                }
            }
        }

        public static void HandleCollisions(Entity entity, Entity otherEntity)
        {
            if(!entity.IsCollidable || !otherEntity.IsCollidable || !entity.IsAlive || !otherEntity.IsAlive)
            {
                return;
            }

            entity.HandleCollision(otherEntity);
            otherEntity.HandleCollision(entity);
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