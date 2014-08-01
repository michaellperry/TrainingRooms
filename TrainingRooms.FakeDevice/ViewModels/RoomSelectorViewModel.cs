using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TrainingRooms.Logic;
using TrainingRooms.Model;
using UpdateControls.Fields;
using UpdateControls.XAML;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class RoomSelectorViewModel : IScreen
    {
        private readonly SignDevice _device;
        private Independent<Room> _selectedRoom = new Independent<Room>();

        public RoomSelectorViewModel(SignDevice device)
        {
            _device = device;
        }

        public IEnumerable<RoomHeader> Rooms
        {
            get
            {
                return
                    from room in _device.VenueToken.Venue.Value.Rooms
                    orderby room.Name.Value
                    select new RoomHeader(room);
            }
        }

        public RoomHeader SelectedRoom
        {
            get { return _selectedRoom == null ? null : new RoomHeader(_selectedRoom); }
            set { _selectedRoom.Value = value == null ? null : value.Room; }
        }

        public ICommand Lock
        {
            get
            {
                return MakeCommand
                    .When(() => _selectedRoom.Value != null)
                    .Do(delegate
                    {
                        _device.SelectedRoom = _selectedRoom;
                    });
            }
        }
    }
}
