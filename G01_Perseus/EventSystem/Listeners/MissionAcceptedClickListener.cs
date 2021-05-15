using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    public interface MissionAcceptedClickListener : EventListener
    {
        void OnAccepted(MissionAcceptedClickEvent e);
    }
}
