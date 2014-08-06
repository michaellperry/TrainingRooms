using System;

namespace TrainingRooms.Device
{
    public interface IDispatchOnUIThread
    {
        void Invoke(Action action);
    }
}
