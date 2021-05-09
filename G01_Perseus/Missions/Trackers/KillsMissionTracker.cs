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

        /// <summary>
        /// If owner Entity is not known at tracker creation.
        /// </summary>
        /// <param name="typeOfSubject"></param>
        /// <param name="tasksToComplete"></param>
        public KillsMissionTracker(Type typeOfSubject, int tasksToComplete) : base(tasksToComplete)
        {
            this.typeOfSubject = typeOfSubject;

            EventManager.Register(this);
        }

        /// <summary>
        /// If owner Entity is known at tracker creation.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="typeOfSubject"></param>
        /// <param name="tasksToComplete"></param>
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
                if (/*Owner*/TypeOfBullet.Player == (e.AttackerEntity as Bullet).Type /* <-- kan hanteras bättre */ && typeOfSubject == e.SubjectEntity.GetType())
                {
                    TaskComplete();
                    TaskComplete(); // Remove this
                }
            }
        }

        public void AddOwner(Entity owner)
        {
            Owner = owner;
        }
    }
}
