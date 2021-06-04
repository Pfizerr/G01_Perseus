using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using G01_Perseus.EventSystem.Events;

namespace G01_Perseus
{
    public static class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();
        public static List<Faction> factions = new List<Faction>();
        public static Player Player;

        public static Level Level = null;
        public static void ReInit()
        {
            entities.Clear();
            factions.Clear();
            Player = null;

            AddFaction(new Faction("test faction one", AssetManager.SpriteAsset("planet1"), AssetManager.SpriteAsset("planet_highlight_outline2_green")));
            AddFaction(new Faction("test faction two", AssetManager.SpriteAsset("planet2"), AssetManager.SpriteAsset("planet_highlight_outline2_blue")));
            AddFaction(new Faction("test faction three", AssetManager.SpriteAsset("planet3"), AssetManager.SpriteAsset("planet_highlight_outline2_red")));
            factions[0].CreatePlanet("planet name one", new Vector2(0, 0), 1);
            factions[1].CreatePlanet("planet name two", new Vector2(1000, 3000), 2);
            factions[2].CreatePlanet("planet name three", new Vector2(-2000, 1750), 3);

            CreatePlayer();
        }

        public static void CreatePlayer()
        {
            Player = new Player(new Vector2(250, 250), new Vector2(500, 500), new Vector2(0.2f, 0.2f), 100, 100, AssetManager.TextureAsset("player_ship"), 1, 1);
            entities.Add(Player);
        }

        public static Enemy CreateEnemyOrbital(Vector2 position)
        {
            Enemy enemy = new Enemy(position, new Vector2(500, 500), new Vector2(0.2f, 0.2f), 10, 0, new DefaultEnemyBehavior(), 1000, AssetManager.TextureAsset("enemy_ship"), 4, 1, 1);
            entities.Add(enemy);
            return enemy;
            
        }

        public static Enemy CreateEnemyRaptor(Vector2 position)
        {
            Enemy enemy = new Enemy(position, new Vector2(500, 500), new Vector2(0.3f, 0.3f), 20, 0, new RaptorEnemyBehavior(), 3000, AssetManager.TextureAsset("enemy_ship2"), 10, 1, 1);
            entities.Add(enemy);
            return enemy;
            
        }

        public static Enemy CreateEnemyPursuer(Vector2 position)
        {
            Enemy enemy = new Enemy(position, new Vector2(500, 500), new Vector2(0.4f, 0.4f), 40, 0, new DefaultEnemyBehavior(), 10000, AssetManager.TextureAsset("enemy_ship3"), 5, 1, 1);
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

        public static void CreateLaser(TypeOfLaser type, Vector2 start, Vector2 target, float damage)
        {
            AddLaser(new Laser(start, target, new Vector2(0.1f, Vector2.Distance(start, target)), type, damage, 900, AssetManager.TextureAsset("gradient_bar")));
            //AddBullet(new Bullet(start, target, Vector2.Zero, new Vector2(10, 0.1f/*Vector2.Distance(start, target)*/), type, damage, 1));
        }

        public static void AddBullet(Bullet bullet) => entities.Add(bullet);

        public static void AddLaser(Laser laser) => entities.Add(laser);

        public static void AddFaction(Faction faction) => factions.Add(faction);

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].IsAlive)
                {
                    entities[i].Destroy(null);
                    if (entities[i] is Player)
                    {
                        EventManager.Dispatch(new PlayerDeathEvent());
                    }
                    if (!(entities[i] is Explosion) && !(entities[i] is Bullet) && !(entities[i] is Laser))
                    {
                        entities.Add(new Explosion(entities[i].Position, Vector2.Zero, 5, AssetManager.TextureAsset("smokeParticle_sprite")));
                    }
                    entities.RemoveAt(i);
                    continue;
                }

                entities[i].Update(gameTime);
            }

            for (int i = 0; i < factions.Count; i++)
            {
                factions[i].Update(gameTime);
            }

            int x = (int)Player.Position.X / Level.tileWidth;
            int y = (int)Player.Position.Y / Level.tileHeight;

            if(Player.Sector != Level.GetSectorCoordinates(x, y))
            {
                Player.Sector = Level.GetSectorCoordinates(x, y);
                EventManager.Dispatch(new SectorEnteredEvent(Player, Player.Sector));
            }

            CheckEntityCollisions();

        }

        public static void CheckEntityCollisions()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i] is Laser laser)
                {
                    List<Vector2> laserPositions = laser.FindAllReasonablePositions();
                    List<Entity> targets = new List<Entity>();
                    for (int j = 0; j < entities.Count; j++)
                    {


                        foreach (Vector2 vector2 in laserPositions)
                        {

                            if (entities[j].HitBox.Contains(vector2) && !(entities[j] is Player)) // Temporary Bandaid
                            {
                                //Console.WriteLine(entities[j]);
                                targets.Add(entities[j]);
                            }

                        }


                    }
                    List<Entity> uniqueTargets = targets.Distinct().ToList();
                    for (int l = 0; l < uniqueTargets.Count; l++)
                    {
                        //uniqueTargets[l].HandleCollision(entities[i]);
                        Console.WriteLine(uniqueTargets[l]);
                        EventManager.Dispatch(new CollissionEvent(uniqueTargets[l], entities[i]));
                    }
                }
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