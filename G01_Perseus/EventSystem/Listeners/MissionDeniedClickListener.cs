﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    public interface MissionDeniedClickListener : EventListener
    {
        void OnDenied(MissionDeniedClickEvent e);
    }
}
