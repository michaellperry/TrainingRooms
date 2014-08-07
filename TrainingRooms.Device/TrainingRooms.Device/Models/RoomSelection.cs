using TrainingRooms.Model;
using UpdateControls.Fields;

namespace TrainingRooms.Device.Models
{
    public class RoomSelection
    {
        private Independent<Room> _selectedRoom = new Independent<Room>();

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom.Value = value; }
        }
    }
}
