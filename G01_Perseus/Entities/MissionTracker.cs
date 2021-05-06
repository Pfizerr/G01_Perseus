using System;
using Microsoft.Xna.Framework;
namespace G01_Perseus
{
    public class MissionTracker /*: EventListener*/ 
    {
        /// <summary>
        /// Counter for the amount of times the associated event has been raised.
        /// </summary>
        public int Progress
        {
            get;
            private set;
        }

        public bool IsCompleted
        {
            get;
            private set;
        }

        private int id;
        private int eventsRaised;
        private int eventsRaisedToComplete;
        private Entity owner;
        private Entity subject;
        // private Event eventToTrack;

        public MissionTracker(int id, int countToComplete, Entity owner, Entity subject)
        {
            this.id = id;
            this.eventsRaisedToComplete = countToComplete;
            this.owner = owner;
            this.subject = subject;

            //EventManager.Register(this);
            eventsRaised = 0;
        }

        public MissionTracker(string[] data)
        {

        }

        public void Update()
        {
            if (eventsRaised >= eventsRaisedToComplete)
            {
                IsCompleted = true;
            }
            Console.WriteLine(String.Format("Mission progress detected. Mission ID: {0}", id));
        }

        public void OnNotify()
        {
            Console.WriteLine(String.Format("Mission progress detected. Mission ID: {0}", id));
           
            #region pseudo
            //if (_event.RaisedBy != ownerToTrack || _event.Other != entityToTrack)
            //{
            //    throw new NotImplementedException();
            //}

            Progress++;

            if (Progress > eventsRaisedToComplete)
            {
                IsCompleted = true;
            }
            #endregion
        }
    }
}