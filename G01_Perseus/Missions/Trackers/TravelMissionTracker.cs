using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using Microsoft.Xna.Framework;

namespace G01_Perseus.Missions.Trackers
{
    public class TravelMissionTracker : MissionTracker, SectorEnteredListener
    {
        private Point sector;
        public TravelMissionTracker(Point sector, int tasksToComplete) : base(tasksToComplete)
        {
            this.sector = sector;

            EventManager.Register(this);
        }

        public void OnSectorEntered(SectorEnteredEvent e)
        {
            if(IsActive && e.Entity == Owner && e.Sector == sector)
            {
                TaskComplete();
            }
        }
    }
}
