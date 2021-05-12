using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public static class Resources
    {
        public static int Currency { get; private set; }
        public static int Dust { get; private set; }
        public static int SkillPoints { get; private set; }
        public static float SpDamage { get; private set; }
        public static float SpHealth { get; private set; }
        public static float SpShields { get; private set; }
        public static float SpFireRate { get; private set; }
        public static float MaxPoints { get; private set; } 

        public static void Initialize(int currency, int dust, int sp, int spDamage, int spShields, int spHealth, int spFireRate)
        {
            Currency = currency;
            Dust = dust;
            SpDamage = spDamage;
            SpHealth = spHealth;
            SpShields = spShields;
            SpFireRate = spFireRate;
            SkillPoints = 5; //Replace with sp from the parameters
            MaxPoints = 10;
        }

        public static void AddCurrency(int amount)
        {
            Currency += amount;
        }

        public static void DecreaseCurrency(int amount)
        {
            Currency -= amount;
        }

        public static void AddDust(int amount)
        {
            Dust += amount;
        }

        public static void DecreaseDust(int amount)
        {
            Dust -= amount;
        }

        public static void AddSkillPoint(int amount)
        {
            SkillPoints += amount;
        }

        public static void DecreaseSkillPoints()
        {
            SkillPoints -= 1;
        }

        public static void ResetSkillpoints()
        {
            SkillPoints += (int)(SpDamage + SpHealth + SpShields);
            SpDamage = 0;
            SpHealth = 0;
            SpShields = 0;
        }

        public static void AddSpDamage()
        {
            if(SkillPoints > 0 || SpDamage < 10)
            {
                SpDamage += 1;
                DecreaseSkillPoints();
                Console.WriteLine(SpDamage);
            }
            else
            {
                Console.WriteLine("Not enough SP");
            }
                
            
        }

        public static void AddSpHealth()
        {
            if (SkillPoints > 0 || SpHealth < 10)
            {
                SpHealth += 1;
                DecreaseSkillPoints();
            }
                
        }

        public static void AddSpShields()
        {
            if (SkillPoints > 0 || SpShields < 10)
            {
                SpShields += 1;
                DecreaseSkillPoints();
            }
                
        }

        public static void AddSpFireRate()
        {
            if (SkillPoints > 0 || SpFireRate < 10)
            {
                SpFireRate += 1;
                DecreaseSkillPoints();
            }
                
        }
    }
}
