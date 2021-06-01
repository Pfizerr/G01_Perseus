using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using G01_Perseus.EventSystem.Events;

namespace G01_Perseus
{
    public static class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();
        public static List<Faction> factions = new List<Faction>();
        public static Player Player;

        public static void CreatePlayer()
        {
            Player = new Player(new Vector2(250, 250), new Vector2(500, 500), new Vector2(0.2f, 0.2f), 100, 100, AssetManager.TextureAsset("player_ship"));
            entities.Add(Player);
        }

        public static Enemy CreateEnemyOrbital(Vector2 position)
        {
            Enemy enemy = new Enemy(position, new Vector2(0, 0), new Vector2(0.2f, 0.2f), 10, 0, new DefaultEnemyBehavior(), 1000, AssetManager.TextureAsset("enemy_ship"), 4);
            entities.Add(enemy);
            return enemy;
        }

        public static Enemy CreateEnemyRaptor(Vector2 position)
        {
            Enemy enemy = new Enemy(position, new Vector2(0, 0), new Vector2(0.3f, 0.3f), 20, 0, new RaptorEnemyBehavior(), 3000, AssetManager.TextureAsset("enemy_ship2"), 10);
            entities.Add(enemy);
            return enemy;
        }

        public static Enemy CreateEnemyPursuer(Vector2 position)
        {
            Enemy enemy = new Enemy(position, new Vector2(0, 0), new Vector2(0.4f, 0.4f), 40, 0, new DefaultEnemyBehavior(), 10000, AssetManager.TextureAsset("enemy_ship3"), 5);
            entities.Add(enemy);
            return enemy;
        }

        public static void CreateEnemySpawner(Vector2 position, EnemySpawner.Type type, int spawnTimerMs)
        {
            entities.Add(new EnemySpawner(type, position, spawnTimerMs));
        }

        public static void CreateBullet(TypeOfBullet type, Vector2 start, Vector2 target, float damage)
        {
            AddBullet(new Bullet(start, target, new Vector2(7, 7), new Vector2(0.1f, 0.1f), type, damage, 10, AssetManager.TextureAsset("projectile_green")));
        }

        //public static void CreateLaser(TypeOfLaser type, Vector2 start, Vector2 target, float damage)
        //{
        //    AddLaser(new Laser(start, target, new Vector2(0.1f, Vector2.Distance(start, target)), type, damage, 900));
        //    AddBullet(new Bullet(start, target, Vector2.Zero, new Vector2(10, 0.1f/*Vector2.Distance(start, target)*/), type, damage, 1));
        //}

        public static void AddBullet(Bullet bullet) => entities.Add(bullet);

        //public static void AddLaser(Laser laser) => entities.Add(laser);

        public static void AddFaction(Faction faction) => factions.Add(faction);

        public static void Update(GameTime gameTime)
        {
            if(!Player.IsAlive)
            {
                EventManager.Dispatch(new PushStateEvent(new RespawnMenu(null)));
            }

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