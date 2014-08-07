using System;
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

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            var that = obj as RoomHeader;
            if (that == null)
                return false;

            return Object.Equals(this._room, that._room);
        }

        public override int GetHashCode()
        {
            return _room.GetHashCode();
        }
    }
}
