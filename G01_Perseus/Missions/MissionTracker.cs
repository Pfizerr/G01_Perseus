using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class MissionTracker
    {
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
            set;
        }

        public virtual bool IsActive
        {
            get;
            set;
        }

        public bool IsCompleted
        {
            get;
            protected set;
        }

        public MissionTracker(int tasksToComplete)
        {
            this.TasksToComplete = tasksToComplete;

            this.IsCompleted = false;
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
