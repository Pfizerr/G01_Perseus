using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class Equipment
    {
        protected int baseValue;
        protected int instanceId;
        protected Entity parent;

        public Equipment(int baseValue, Entity parent)
        {
            this.parent = parent;
            this.baseValue = baseValue;
            instanceId = EquipmentManager.GetUniqueId();
        }
        
        public virtual int InstanceId { get => instanceId; private set => instanceId = value; }
    }
}
