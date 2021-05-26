using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class PursuerEnemyBehavior : DefaultEnemyBehavior
    {
        Timer timer = new Timer(2000);

        public PursuerEnemyBehavior()
        {
            pursueDistance = 100;
            pursueDistance = 50;
        }

        
    }
}