using System;
using Microsoft.xna.Framework;

namespace G01_Perseus
{
    public enum EMissionState
    {
        Offered,
        Accepted,
        NotUsed
    }

    public class MissionData
    {
        public int Id 
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Entity Owner
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public EMissionState State
        {
            get;
            private set;
        }

        public Planet Contractor // Add in constructor
        {
            get;
            private set;
        }

        public MissionData(int id, string name, Entity owner, string description, EMissionState state)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Description = description;
            State = state;
        }

        public MissionData(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

            Owner = null;
            State = EMissionState.NotUsed;
        }
    }

    public class Event
    {
        public Entity RaisedBy
        {
            get;
            private set;
        }

        public Entity Other
        {
            get;
            private set;
        }

        public Event(Entity raisedBy, Entity other)
        {
            RaisedBy = raisedBy;
            Other = other;
        }
    }

    public class MissionTracker
    {
        private int id;
        private int progress; // counter for the amount of times that the relevant event has been raised
        //private Event eventToTrack;
        private int condition;
        private Entity ownerToTrack;
        private Entity entityToTrack;
        private bool isConditionsFulfilled;

        public void OnNotify(Event event)
        {
            //Check to see if everything is correct
            if(event.RaisedBy != ownerToTrack || event.Other != entityToTrack)
            {
                throw new InvalidOperationException();
            }

            progress++;

            if(progress > condition)
            {
                isConditionsFulfilled = true;
                
                //Unsubscribe from relevant event
            }

            // EventListeners are only notified once per event, i assume
        }
        
        public int Progress
        {
            get { return progress; }
            private set { progress = value; }
        }

        public int IsConditionsFulfilled
        {
            get { return isConditionsFulfilled; }
            private set { isConditionsFulfilled = value; }
        }
    }

    public class Mission
    {
        MissionData data;
        MissionTracker tracker;

        public Mission(int id, string name, string description)
        {
            data = new MissionData(id, name, description);
        }

        public Mission(MissionData data)
        {
            this.data = data;
        }

        public void Update()
        {
            if(data.State == EMissionState.NotUsed)
            {
                return;
            }

            if(data.State == EMissionState.Offered)
            [
                // Offered by contractor/planet
            ]

            if(data.State == EMissionState.Active)
            {
                if(tracker.IsConditionsFulfilled)
                {
                    // Stop tracking and prepare for mission turn-in.
                }
            }
        }

    }
}