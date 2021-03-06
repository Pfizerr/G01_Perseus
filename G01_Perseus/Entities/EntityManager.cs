using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace G01_Perseus
{
    public static class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();
        public static List<Faction> factions = new List<Faction>();
        public static Player Player;

        public static void CreatePlayer()
        {
            Player = new Player(new Vector2(250, 250), new Vector2(500, 500), new Vector2(0.2f, 0.2f), 100, 100);
            entities.Add(Player);
        }

        public static void CreateEnemy(Vector2 position)
        {
            entities.Add(new Enemy(position, new Vector2(0, 0), new Vector2(0.2f, 0.2f), 1, 0, new DefaultEnemyBehavior()));
        }

        public static void CreateBullet(TypeOfBullet type, Vector2 start, Vector2 target, bool isLaser, float damage)
        {
            AddBullet(new Bullet(start, target, new Vector2(7, 7), new Vector2(0.1f, 0.1f), type, damage, 10));
        }

        public static void AddBullet(Bullet bullet) => entities.Add(bullet);

        public static void AddFaction(Faction faction) => factions.Add(faction);

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].IsAlive)
                {
                    entities[i].Destroy(null);
                    entities.RemoveAt(i);
                    continue;
                }

                entities[i].Update(gameTime);
            }

            for (int i = 0; i < factions.Count; i++)
            {
                factions[i].Update(gameTime);
            }

            CheckEntityCollisions();

        }

        public static void CheckEntityCollisions()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = 0; j < entities.Count; j++)
                {
                    if ((entities[i] == entities[j]) || (entities[i] is Bullet && entities[j] is Bullet))
                    {
                        continue;
                    }

                    if (entities[i].HitBox.Intersects(entities[j].HitBox))
                    {
                        // Collision Event
                        //Console.WriteLine(entities[i].ToString() + " collided with " + entities[j].ToString());
                        EventManager.Dispatch(new CollissionEvent(entities[i], entities[j]));
                        //HandleCollisions(entities[i], entities[y]);
                    }
                }
            }
        }

        public static void HandleCollisions(Entity entity, Entity otherEntity)
        {
            if (!entity.IsCollidable || !otherEntity.IsCollidable || !entity.IsAlive || !otherEntity.IsAlive)
            {
                return;
            }

            entity.HandleCollision(otherEntity);
            otherEntity.HandleCollision(entity);

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(spriteBatch, 0, 0, 0, 0, 0, 0);
            }

            for (int i = 0; i < factions.Count; i++)
            {
                factions[i].Draw(spriteBatch);
            }
        }

    }
}