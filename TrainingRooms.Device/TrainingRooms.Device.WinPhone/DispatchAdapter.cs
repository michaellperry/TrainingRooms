using System;
using System.Windows;

namespace TrainingRooms.Device.WinPhone
{
    public class DispatchAdapter : IDispatchOnUIThread
    {
        public void Invoke(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
