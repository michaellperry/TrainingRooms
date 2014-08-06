using System;
using UpdateControls;

namespace TrainingRooms.Device.Dependency
{
    public class DependentSubscription : IUpdatable
    {
        private readonly Dependent _dependent;
        private readonly Action _update;
        
        public DependentSubscription(Dependent dependent, Action update)
        {
            _dependent = dependent;
            _update = update;

            _dependent.Invalidated += OnInvalidated;
            _update();
        }

        public void Unsubscribe()
        {
            _dependent.Invalidated -= OnInvalidated;
        }

        private void OnInvalidated()
        {
            UpdateScheduler.ScheduleUpdate(this);
        }

        void IUpdatable.UpdateNow()
        {
            _update();
        }
    }
}
