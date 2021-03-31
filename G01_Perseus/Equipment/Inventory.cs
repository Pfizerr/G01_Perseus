using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Inventory
    {
        private List<Equipment> equipment;
        private Equipment[] activeEquipment;
        private int inventorySize;
        private int activeEquipmentSlots;

        public Inventory(int inventorySize, int activeEquipmentSlots)
        {
            this.inventorySize = inventorySize;
            this.activeEquipmentSlots = activeEquipmentSlots;

            equipment = new List<Equipment>();
            activeEquipment = new Equipment[activeEquipmentSlots];
        }

        public Inventory(List<Equipment> inventoryState)
        {
            this.equipment = inventoryState;
            this.inventorySize = inventoryState.Count;

            activeEquipment = new Equipment[activeEquipmentSlots];
        }

        public bool AddEquipment(Equipment equipment)
        {
            if(this.equipment.Count >= inventorySize)
            {
                return false;
            }

            this.equipment.Add(equipment);
            return true;
        }

        public void ActivateEquipment(Equipment equipment, int slot)
        {
            if (activeEquipment == null)
            {
                return;
            }

            if (activeEquipment[slot] != null)
            {
                // Overwrite equipment on given slot.
            }

            activeEquipment[slot] = equipment;
        }

        public void TriggerOffensiveEquipment()
        {
            foreach(Equipment a in activeEquipment)
            {
                if(a is Weapon)
                {
                    (a as Weapon).Activate();
                }
            }
        }

        public void TriggerDefensiveEquipment()
        {
            foreach(Equipment a in activeEquipment)
            {
                //if(a is Defensive...)
            }
        }

        public List<Equipment> Equipment { get => equipment; private set => equipment = value; }
    }
}
