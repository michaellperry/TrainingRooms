using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Event> NewEventAsync(Group group, DateTime time, int startMinutes)
        {
            var schedule = await ScheduleForAsync(time.Date);
            var upcommingEvent = await Community.AddFactAsync(new Event());
            upcommingEvent.Group = group;
            upcommingEvent.StartMinutes = startMinutes;
            await Community.AddFactAsync(new EventSchedule(
                upcommingEvent, schedule, Enumerable.Empty<EventSchedule>()));
            return upcommingEvent;
        }

        public async Task DeleteAsync()
        {
            await Community.AddFactAsync(new RoomDelete(this));
        }
    }
}
