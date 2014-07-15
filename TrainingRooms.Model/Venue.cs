using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Venue
    {
        public async Task<Room> NewRoomAsync()
        {
            return await Community.AddFactAsync(new Room(this));
        }

        public async Task<Group> NewGroup()
        {
            return await Community.AddFactAsync(new Group(this));
        }
    }
}
