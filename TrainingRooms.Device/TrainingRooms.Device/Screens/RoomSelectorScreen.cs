using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using TrainingRooms.Device.ViewModels;
using TrainingRooms.Logic;
using UpdateControls.Fields;
using System.Linq;
using System.Threading;
using TrainingRooms.Device.Dependency;

namespace TrainingRooms.Device.Screens
{
    public class RoomSelectorScreen : ViewModelBase, IScreen
    {
        private readonly SignDevice _device;

        public RoomSelectorScreen(SignDevice device)
        {
            _device = device;
        }

        public string Status
        {
            get
            {
                return Get(() =>
                {
                    if (_device.Community.Synchronizing)
                        return "Synchronizing...";

                    var lastException = _device.Community.LastException;
                    if (lastException != null)
                        return _device.Community.LastException.Message;

                    return "Ready";
                });
            }
        }

        public IEnumerable<RoomHeader> Rooms
        {
            get
            {
                return GetCollection(() =>
                    from room in _device.VenueToken.Venue.Value.Rooms
                    select new RoomHeader(room));
            }
        }
    }
}
