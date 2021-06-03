using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class MissionTracker
    {
        public MissionTracker(int tasksToComplete)
        {
            this.TasksToComplete = tasksToComplete;

            this.IsCompleted = false;
        }

        public virtual Entity Owner
        {
            get;
            set;
        }

        public virtual int TasksToComplete
        {
            get;
            protected set;
        }

        public virtual int TasksCompleted
        {
            get;
            protected set;
        }

        public bool IsCompleted
        {
            get;
            protected set;
        }

        public virtual bool IsActive
        {
            get;
            set;
        }

        public virtual Point Sector
        {
            get;
            set;
        }

        protected virtual void TaskComplete()
        {
            TasksCompleted++;

            if(TasksCompleted >= TasksToComplete)
            {
                IsCompleted = true;
                IsActive = false;
            }
        }
    }
}
