using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public interface MouseEnterPlanetListener : EventListener
    {

        void OnMouseEnter(PlanetHighlightEvent e);

    }
}
