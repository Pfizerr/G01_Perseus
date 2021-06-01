using System;
using Microsoft.Xna.Framework;

namespace G01_Perseus
{
    public class Mission
    {
        public enum EState
        {
            Offered,
            Accepted,
            Completed,
            NotInUse
        }        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EState State { get; set; }
        public Planet Contractor { get; set; }
        public MissionTracker Tracker { get; set; }
        public Entity Owner { get; private set; }
        public int Resources { get; set; }
        public int Dust { get; set; }
        public int SkillPoints { get; set; }
        public Equipment Equipment { get; set; }

        public Mission()
        {
            State = EState.NotInUse;
        }

        public void SetOwner(Entity owner)
        {
            if (Tracker.Owner == null)
            {
                Tracker.Owner = owner;
                Tracker.IsActive = true;
                State = EState.Accepted;
            }

            Owner = owner;
        }

        public string Text() => string.Format("Name: {0}\nDescription: {1}\nRewards: {2}", Name, Description, Resources.ToString());

        public void Update()
        {
            switch(State)
            {
                case EState.Accepted:
                    if(Tracker.IsCompleted)
                    {
                        State = EState.Completed;
                    }
                    else
                    {
                        if(Tracker.Owner != null && !Tracker.IsActive)
                        {
                            Tracker.IsActive = true;
                        }
                    }
                    break;
                case EState.Offered:
                    
                    break;
                case EState.NotInUse:

                    break;
            }
        }
    }
}