using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static G01_Perseus.UI.UIButton;

namespace G01_Perseus.UI
{
    public class MissionUIElement : UIButton
    {
        private bool isOnDisplay;

        public MissionUIElement(Mission mission, Rectangle bounds, OnClick onClick) : base (bounds, onClick)
        {
            this.mission = mission;

            this.isOnDisplay = true;
        }

        public Mission mission { get; private set; }
    }
}
