using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class PlayerStatus
    {

        private float maxHealth;
        private float health;

        private float maxShields;
        private float shields;
        
        private int skillPoints;
        private int level;
        private int experience;
        private int resources;
        private int dust;
        
        /// <summary>
        /// New player status constructor
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxShields"></param>
        public PlayerStatus(float maxHealth, float maxShields)
        {
            this.maxHealth = maxHealth;
            this.maxShields = maxShields;
        }

        /// <summary>
        /// Existing player status constructor
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxShields"></param>
        /// <param name="skillPoints"></param>
        /// <param name="level"></param>
        /// <param name="experience"></param>
        /// <param name="resources"></param>
        /// <param name="dust"></param>
        public PlayerStatus(float maxHealth, float maxShields, int skillPoints, int level, int experience, int resources, int dust)
        {
            this.maxHealth = maxHealth;
            this.maxShields = maxShields;
            this.skillPoints = skillPoints;
            this.level = level;
            this.experience = experience;
            this.resources = resources;
            this.dust = dust;
        }

        public void Reward(int skillPoints, int resources, int dust)
        {
            this.skillPoints += skillPoints;
            this.resources += resources;
            this.dust += dust;
        }

        
    }
}
