using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class EnemySpawner : Entity
    {

        public enum Type
        {
            Orbital,
            Raptor,
            Pursuer
        }

        private Enemy enemy;
        private Type type;
        private Timer timer;

        private Texture2D debugTexture;

        public EnemySpawner(Type type, Vector2 position, int respawnTimeMs) : base (position, Vector2.One, Util.CreateRectangleTexture(1, 1, Color.Transparent, Color.Transparent))
        {
            this.type = type;
            this.timer = new Timer(respawnTimeMs);

            debugTexture = Util.CreateRectangleTexture(50, 50, Color.Green, Color.Transparent);
        }

        public override void Update(GameTime gameTime)
        {           

            if(enemy == null)
            {
                if (timer.IsDone(gameTime))
                {
                    enemy = SpawnEnemy(type);
                }
            }
            else if (!enemy.IsAlive)
            {
                enemy = null;
            }
            else
            {
                timer.Reset(gameTime);
            }
        }

        private Enemy SpawnEnemy(Type type)
        {
            switch (type)
            {
                case Type.Orbital: return EntityManager.CreateEnemyOrbital(Position);
                case Type.Raptor: return EntityManager.CreateEnemyRaptor(Position);
                case Type.Pursuer: return EntityManager.CreateEnemyPursuer(Position);
            }
            return EntityManager.CreateEnemyOrbital(Position);
        }

        public override void Draw(SpriteBatch spriteBatch, int tileX, int tileY, int ix, int iy, int tileWidth, int tileHeight)
        {
            //spriteBatch.Draw(debugTexture, Position, Color.White);
        }

        public override void HandleCollision(Entity other)
        {
            
        }

       
    }
}
