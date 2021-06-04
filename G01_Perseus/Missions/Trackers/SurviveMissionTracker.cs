using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using Microsoft.Xna.Framework;

namespace G01_Perseus.Missions.Trackers 
{
    public class SurviveMissionTracker : MissionTracker, SectorEnteredListener
    {
        private Point sector;
        private int timeToSurvive;
        private ExtendedTimer timer;

        public SurviveMissionTracker(Point sector, int timeToSurvive, int tasksToComplete) : base(tasksToComplete)
        {
            this.sector = sector;
            this.timeToSurvive = timeToSurvive;

            this.timer = new ExtendedTimer(timeToSurvive, false);
        }

        public void OnSectorEntered(SectorEnteredEvent e)
        {
            if (e.Sector == sector && !timer.IsCounting)
            {
                timer.Start();
            }
            else if (timer.IsCounting)
            {
                timer.Stop();
            }
        }
    }
}
