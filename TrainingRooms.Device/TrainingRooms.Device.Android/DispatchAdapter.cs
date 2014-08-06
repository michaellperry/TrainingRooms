using Android.App;
using System;

namespace TrainingRooms.Device.Droid
{
    public class DispatchAdapter : IDispatchOnUIThread
    {
        public readonly Activity owner;

        public DispatchAdapter(Activity owner)
        {
            this.owner = owner;
        }

        public void Invoke(Action action)
        {
            owner.RunOnUiThread(action);
        }
    }
}