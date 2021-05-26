using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class EnemyBehavior
    {
        public Enemy Enemy { get; set; }

        protected int pursueDistance, retreatDistance;

        public EnemyBehavior()
        {

        }
        

        public abstract void Update(GameTime gameTime);
    }


}