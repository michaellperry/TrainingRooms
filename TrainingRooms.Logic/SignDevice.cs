using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence.Strategy;
using UpdateControls.Fields;

namespace TrainingRooms.Logic
{
    public class SignDevice : Device
    {
        private Independent<Room> _selectedRoom = new Independent<Room>(
            Room.GetNullInstance());

        public SignDevice(IStorageStrategy storage, DateSelectionModel dateSelectionModel)
            : base(storage, dateSelectionModel)
        {
        }

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom.Value = value; }
        }
    }
}
