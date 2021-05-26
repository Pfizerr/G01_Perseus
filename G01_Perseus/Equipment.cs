using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class Equipment
    {
        protected string name;
        protected int iD;

        public Equipment(int iD)
        {
            this.iD = iD;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

    }
}