using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public static class EquipmentManager
    {
        private static int equipmentCount;

        public static int GetUniqueId()
        {
            return equipmentCount++;
        }
    }
}
