using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Venue
    {
        public async Task<Room> NewRoom()
        {
            return await Community.AddFactAsync(new Room(this));
        }
    }
}
