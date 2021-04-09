using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public abstract class Event
    {
        public abstract void Dispatch(EventListener listener);
    }
    
    public interface EventListener
    {

    }

    public static class EventManager
    {
        private static List<EventListener> listeners = new List<EventListener>();
        
        public static void Register(EventListener listener)
        {
            listeners.Add(listener);
        }

        public static void Unregister(EventListener listener)
        {
            listeners.Remove(listener);
        }

        public static void Dispatch(Event e)
        {
            foreach(EventListener listener in listeners)
            {
                e.Dispatch(listener);
            }
        }
    }
}
