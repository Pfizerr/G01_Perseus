﻿using Microsoft.Xna.Framework;
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


        public abstract void Update(GameTime gameTime, float rotation);
    }


}