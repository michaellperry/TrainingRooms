using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Event
    {
        public async Task Delete()
        {
            await Community.AddFactAsync(new EventDelete(this));
        }
    }
}
