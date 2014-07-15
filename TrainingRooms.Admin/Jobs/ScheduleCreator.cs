using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.Jobs
{
    public class ScheduleCreator
    {
        private readonly List<Room> _rooms;
        private readonly DateTime _date;

        public ScheduleCreator(IEnumerable<Room> rooms, DateTime date)
        {
            _rooms = new List<Room>(rooms);
            _date = date;
        }

        public async Task<Schedule[]> CreateSchedulesAsync()
        {
            if (_rooms.Any())
            {
                var community = _rooms.First().Community;
                var day = await community.AddFactAsync(new Day(_date));
                var schedules = await Task.WhenAll(_rooms
                    .Select(r => community.AddFactAsync(new Schedule(r, day))));
                return schedules;
            }
            else
                return new Schedule[0];
        }
    }
}
