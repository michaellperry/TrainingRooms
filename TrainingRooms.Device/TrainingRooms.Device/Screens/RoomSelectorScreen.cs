using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using TrainingRooms.Logic;
using UpdateControls.Fields;

namespace TrainingRooms.Device.Screens
{
    public class RoomSelectorScreen : IScreen, INotifyPropertyChanged
    {
        private readonly SignDevice _device;

        private Independent<int> _time = new Independent<int>();
        private Dependent<string> _status;

        private Timer _timer;

        public RoomSelectorScreen(SignDevice device)
        {
            _device = device;

            _status = new Dependent<string>(() => GetStatus());
            _status.Subscribe(s => RaisePropertyChanged(() => Status));

            _timer = new Timer(SetTime, null, 1000, 1000);
        }

        public string Status
        {
            get { return _status; }
        }

        public string GetStatus()
        {
            if (_device.Community.Synchronizing)
            {
                lock (this)
                {
                    return "Synchronizing" + new String(Enumerable.Repeat('.', _time.Value).ToArray());
                }
            }
            var lastException = _device.Community.LastException;
            if (lastException != null)
                return _device.Community.LastException.Message;
            return "Ready";
        }

        private void SetTime(object state)
        {
            lock (this)
            {
                _time.Value++;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var accessor = (MemberExpression)property.Body;
            var propertyName = accessor.Member.Name;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
