using Microsoft.Xna.Framework;

namespace G01_Perseus.Missions.Trackers 
{
    public class SurviveMissionTracker : MissionTracker
    {
        private Point sector;
        private int timeToSurvive;
        public SurviveMissionTracker(Point sector, int timeToSurvive, int tasksToComplete) : base(tasksToComplete)
        {
            this.sector = sector;
            this.timeToSurvive = timeToSurvive;
        }
    }
}
