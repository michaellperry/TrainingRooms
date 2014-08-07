using System.Collections.Generic;
using System.Linq;
using TrainingRooms.Device.Dependency;
using TrainingRooms.Device.Models;
using TrainingRooms.Device.ViewModels;
using TrainingRooms.Logic;

namespace TrainingRooms.Device.Screens
{
    public class RoomSelectorScreen : ViewModelBase, IScreen
    {
        private readonly SignDevice _device;
        private readonly RoomSelection _selection;
        
        public RoomSelectorScreen(SignDevice device, RoomSelection selection)
        {
            _device = device;
            _selection = selection;
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

        public RoomHeader SelectedRoom
        {
            get
            {
                return Get(() => _selection.SelectedRoom == null
                    ? null
                    : new RoomHeader(_selection.SelectedRoom));
            }
            set
            {
                _selection.SelectedRoom = value == null
                    ? null
                    : value.Room;
            }
        }

        public string Selection
        {
            get
            {
                return Get(() => _selection.SelectedRoom == null ? null : _selection.SelectedRoom.Name.Value);
            }
        }
    }
}
