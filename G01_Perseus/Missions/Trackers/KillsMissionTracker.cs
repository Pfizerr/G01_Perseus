using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{

    public class KillsMissionTracker : MissionTracker, EntityKilledListener
    {
        private Type typeOfSubject;

        // If owner Entity is not known at tracker creation.
        public KillsMissionTracker(Type typeOfSubject, int tasksToComplete) : base(tasksToComplete)
        {
            this.typeOfSubject = typeOfSubject;

            EventManager.Register(this);
        }

        // If owner Entity is known at tracker creation.
        public KillsMissionTracker(Entity owner, Type typeOfSubject, int tasksToComplete) : base(tasksToComplete)
        {
            this.typeOfSubject = typeOfSubject;
            
            Owner = owner; 
            EventManager.Register(this);
        }

        public void OnKilled(EntityKilledEvent e)
        {
            if(IsActive)
            {
                if((e.AttackerEntity is Laser) && TypeOfLaser.Player == (e.AttackerEntity as Laser).Type && typeOfSubject == e.SubjectEntity.GetType())
                {
                    TaskComplete();
                }
                else if (/*Owner*/TypeOfBullet.Player == (e.AttackerEntity as Bullet).Type /* <-- kan hanteras bättre */ && typeOfSubject == e.SubjectEntity.GetType())
                {
                    TaskComplete();
                }
            }
        }
    }
}
