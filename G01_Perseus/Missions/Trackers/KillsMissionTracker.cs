using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using Microsoft.Xna.Framework;
using System;

namespace G01_Perseus.Missions.Trackers
{
    public class KillsMissionTracker : MissionTracker, EntityKilledListener
    {
        private Type typeOfSubject;

        // If owner Entity is not known at tracker creation.
        public KillsMissionTracker(Type typeOfSubject, Point sector, int tasksToComplete) : base(tasksToComplete)
        {
            this.typeOfSubject = typeOfSubject;
            this.Sector = sector;
            

            EventManager.Register(this);
        }

        // If owner Entity is known at tracker creation.
        public KillsMissionTracker(Entity owner, Type typeOfSubject, Point sector, int tasksToComplete) : base(tasksToComplete)
        {
            this.typeOfSubject = typeOfSubject;
            this.Sector = sector;
            
            Owner = owner; 
            EventManager.Register(this);
        }

        public void OnKilled(EntityKilledEvent e)
        {
            if(IsActive)
            {
                int x = (int)Owner.Position.X;
                int y = (int)Owner.Position.Y;
                if (/*Owner*/TypeOfBullet.Player == (e.AttackerEntity as Bullet).Type && typeOfSubject == e.SubjectEntity.GetType() && EntityManager.Level.GetSectorCoordinates(x, y) == Sector)
                {
                    TaskComplete();
                }
            }
        }
    }
}
