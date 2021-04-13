using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace G01_Perseus
{
    public static class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();
        public static Player Player;

        public static void CreatePlayer()
        {
            Player = new Player(AssetManager.TextureAsset("player_ship"), new Vector2(250, 250), new Vector2(500, 500), new Vector2(0.2f, 0.2f), null, SpriteEffects.None, Color.White, 0f, 0.85f, true, 100);
            entities.Add(Player);
        }

        public static void CreateEnemy()
        {
            entities.Add(new Enemy(AssetManager.TextureAsset("enemy_ship"), new Vector2(200, 200), new Vector2(400, 400), new Vector2(0.2f, 0.2f), null, SpriteEffects.None, Color.White, 0f, 0.8f, true, 100, 25));
        }

        public static void CreateBullet(Entity parent, Vector2 start, Vector2 target, bool isLaser)
        {
            #region no good
            Vector2 maxBulletVelocity = new Vector2(50, 50);
            Vector2 bulletScale = new Vector2(0.1f, 0.1f);
            Color bulletColor = Color.White;
            float bulletRotation = 0f;
            float bulletLayerDepth = 0.8f;
            bool bulletIsCollidable = true;
            float bulletDamage = 50;
            float bulletTimeToLive = 10;
            #endregion
            EntityManager.AddBullet(new Bullet(AssetManager.TextureAsset("projectile_green"), start, target, maxBulletVelocity, bulletScale, null, SpriteEffects.None, bulletColor, bulletRotation, bulletLayerDepth, bulletIsCollidable, parent, bulletDamage, bulletTimeToLive, isLaser));
        }

        public static void AddBullet(Bullet bullet) => entities.Add(bullet);

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].IsAlive)
                {
                    entities[i].Destroy();
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
        }

    }
}