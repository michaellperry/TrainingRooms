using TrainingRooms.Device.Dependency;
using TrainingRooms.Model;

namespace TrainingRooms.Device.ViewModels
{
    public class RoomHeader : ViewModelBase
    {
        private readonly Room _room;

        public RoomHeader(Room room)
        {
            _room = room;
        }

        public string Name
        {
            get { return Get(() => _room.Name); }
        }
    }
}
