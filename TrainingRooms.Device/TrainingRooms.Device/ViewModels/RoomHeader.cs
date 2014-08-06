using TrainingRooms.Model;

namespace TrainingRooms.Device.ViewModels
{
    public class RoomHeader : ViewModelBase
    {
        private readonly Room _room;

        public RoomHeader(Room room)
        {
            _room = room;

            Bind(() => Name);
        }

        public string Name
        {
            get { return _room.Name; }
        }
    }
}
