using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    class Resources
    {
        public int Currency { get; private set; }
        public int Dust { get; private set; }
        public int SkillPoints { get; private set; }
        public int SpDamage { get; private set; }
        public int SpHealth { get; private set; }
        public int SpShields { get; private set; }
        public int SpTBD { get; private set; }

        public Resources(int currency, int dust, int sp, int spDamage, int spShields, int spHealth)
        {
            Currency = currency;
            Dust = dust;
            SpDamage = spDamage;
            SpHealth = spHealth;
            SpShields = spShields;
        }

        public void AddCurrency(int amount)
        {
            Currency += amount;
        }

        public void DecreaseCurrency(int amount)
        {
            Currency -= amount;
        }

        public void AddDust(int amount)
        {
            Dust += amount;
        }

        public void DecreaseDust(int amount)
        {
            Dust -= amount;
        }

        public void AddSkillPoint(int amount)
        {
            SkillPoints += amount;
        }

        public void DecreaseSkillPoints()
        {
            SkillPoints -= 1;
        }

        public void ResetSkillpoints()
        {
            SkillPoints += (SpDamage + SpHealth + SpShields);
            SpDamage = 0;
            SpHealth = 0;
            SpShields = 0;
        }

        public void AddSpDamage()
        {
            SpDamage += 1;
        }

        public void AddSpHealth()
        {
            SpHealth += 1;
        }

        public void AddSpShields()
        {
            SpShields += 1;
        }
    }
}
