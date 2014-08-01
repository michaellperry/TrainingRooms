using System;
using TrainingRooms.Model;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class RoomHeader
    {
        private readonly Room _room;

        public RoomHeader(Room room)
        {
            _room = room;
        }

        public Room Room
        {
            get { return _room; }
        }

        public string Name
        {
            get { return _room.Name; }
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
    }
}
