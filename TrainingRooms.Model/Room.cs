using System;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Room
    {
        public async Task<Schedule> ScheduleForAsync(DateTime date)
        {
            var day = await Community.AddFactAsync(new Day(date));
            return await Community.AddFactAsync(new Schedule(this, day));
        }

        public async Task DeleteAsync()
        {
            await Community.AddFactAsync(new RoomDelete(this));
        }
    }
}
