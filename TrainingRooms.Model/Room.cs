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

        public async Task<Event> NewEvent(Group group, DateTime time)
        {
            var schedule = await ScheduleFor(time.Date);
            var upcommingEvent = await Community.AddFactAsync(new Event());
            upcommingEvent.Group = group;
            upcommingEvent.Start = time;
            await Community.AddFactAsync(new EventSchedule(
                upcommingEvent, schedule, Enumerable.Empty<EventSchedule>()));
            return upcommingEvent;
        }
    }
}
