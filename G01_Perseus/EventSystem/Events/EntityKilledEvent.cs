using G01_Perseus.EventSystem.Listeners;

namespace G01_Perseus.EventSystem.Events
{
    public class EntityKilledEvent : Event
    {

        // This is the entity that got killed
        public Entity SubjectEntity
        {
            get;
            private set;
        }

        // This is the entity that did the killing
        public Entity AttackerEntity
        {
            get;
            private set;
        }

        public EntityKilledEvent(Entity subjectEntity, Entity otherEntity)
        {
            SubjectEntity = subjectEntity;
            AttackerEntity = otherEntity;
        }

        public override void Dispatch(EventListener listener)
        {
            if (!(listener is EntityKilledListener))
            {
                return;
            }

            EntityKilledListener l = (EntityKilledListener)listener;

            l.OnKilled(this);
        }
    }
}