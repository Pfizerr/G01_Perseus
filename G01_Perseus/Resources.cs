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
        public static int SpDamage { get; private set; }
        public static int SpHealth { get; private set; }
        public static int SpShields { get; private set; }
        public static int SpTBD { get; private set; }

        public static void Initialize(int currency, int dust, int sp, int spDamage, int spShields, int spHealth)
        {
            Currency = currency;
            Dust = dust;
            SpDamage = spDamage;
            SpHealth = spHealth;
            SpShields = spShields;
            SkillPoints = 5;
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
            SkillPoints += (SpDamage + SpHealth + SpShields);
            SpDamage = 0;
            SpHealth = 0;
            SpShields = 0;
        }

        public static void AddSpDamage()
        {
            if(SkillPoints > 0)
            {
                SpDamage += 1;
                Console.WriteLine(SpDamage);
            }
            else
            {
                Console.WriteLine("Not enough SP");
            }
                
            
        }

        public static void AddSpHealth()
        {
            if (SkillPoints > 0)
                SpHealth += 1;
        }

        public static void AddSpShields()
        {
            if (SkillPoints > 0)
                SpShields += 1;
        }
    }
}
