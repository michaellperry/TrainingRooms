using System;
using TrainingRooms.Device.Dependency;
using UpdateControls.Fields;

namespace TrainingRooms.Device
{
    public static class DependentExtensions
    {
        public static DependentSubscription Subscribe<T>(
            this Dependent<T> dependent,
            Action<T> whenChanged)
        {
            return new DependentSubscription(dependent, () => whenChanged(dependent));
        }
    }
}
