using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using TrainingRooms.Device.ViewModels;
using TrainingRooms.Logic;
using UpdateControls.Fields;
using System.Linq;
using System.Threading;

namespace TrainingRooms.Device.Screens
{
    public class RoomSelectorScreen : ViewModelBase, IScreen
    {
        private readonly SignDevice _device;

        private Independent<int> _time = new Independent<int>();

        private Timer _timer;

        public RoomSelectorScreen(SignDevice device)
        {
            _device = device;

            _timer = new Timer(SetTime, null, 1000, 1000);

            Bind(() => Status);
            //Bind(() => Rooms);
        }

        public string Status
        {
            get
            {
                lock (this)
                {
                    return _time.Value.ToString();
                }

                if (_device.Community.Synchronizing)
                    return "Synchronizing...";

                var lastException = _device.Community.LastException;
                if (lastException != null)
                    return _device.Community.LastException.Message;

                return "Ready";
            }
        }

        public List<string> Rooms
        {
            get
            {
                return
                    (from room in _device.VenueToken.Venue.Value.Rooms
                     select room.Name.Value)
                    .ToList();
            }
        }

        private void SetTime(object state)
        {
            lock (this)
            {
                _time.Value++;
            }
        }
    }
}
