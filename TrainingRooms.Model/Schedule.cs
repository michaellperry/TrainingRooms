using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Schedule
    {
        public async Task<Event> NewEventAsync()
        {
            var @event = await Community.AddFactAsync(new Event());
            await Community.AddFactAsync(new EventSchedule(
                @event, this, Enumerable.Empty<EventSchedule>()));
            @event.StartMinutes = 9 * 60;
            @event.EndMinutes = 17 * 60;
            return @event;
        }
    }
}
