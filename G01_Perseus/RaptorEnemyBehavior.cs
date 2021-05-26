using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class RaptorEnemyBehavior : DefaultEnemyBehavior
    {
        Timer timer = new Timer(5000);
       

        public RaptorEnemyBehavior()
        {
            pursueDistance = 200;
            retreatDistance = 100;
        }
       
    }
}