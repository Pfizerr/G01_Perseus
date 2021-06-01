using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public interface HealthChangeListener : EventListener
    {
        void HealthChange(HealthChangeEvent e);
    }
}
