using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Room
    {
        public async Task<Schedule> ScheduleFor(DateTime date)
        {
            var day = await Community.AddFactAsync(new Day(date));
            return await Community.AddFactAsync(new Schedule(this, day));
        }
    }
}
