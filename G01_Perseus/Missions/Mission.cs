using System;
using System.Text;
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
        public int Currency { get; set; }
        public int Dust { get; set; }
        public int Experience { get; set; }
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


        public string Text() => string.Format("Name: {0}\nDescription: {1}\nRewards: {2}", Name, Description, Currency.ToString());

        public string ProgressText() => string.Format("{0}\nProgress: {1} / {2}", Text(), Tracker.TasksCompleted, Tracker.TasksToComplete);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Name: " + Name);
            sb.Append("\nDescription: " + Description + "\n");
            sb.Append("<sector: " + Tracker.Sector + ">\n");


            if (Currency > 0)
            {
                sb.Append(string.Format("Resources: {0},  ", Currency));
            }

            if(Dust > 0)
            {
                sb.Append(string.Format("Dust: {0},  ", Dust));
            }

            if(Experience > 0)
            {
                sb.Append(string.Format("SkillPoints: {0},  ", Experience));
            }

            sb.Append(string.Format("\nProgress: {0} / {1}", Tracker.TasksCompleted, Tracker.TasksToComplete));

            return sb.ToString();
        }

        public void Update() // MOVE TO TRACKER?
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