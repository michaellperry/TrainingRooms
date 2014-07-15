using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.Jobs
{
    public class ScheduleCreationJob
    {
        private readonly List<Room> _rooms;
        private readonly DateTime _date;

        public ScheduleCreationJob(IEnumerable<Room> rooms, DateTime date)
        {
            _rooms = new List<Room>(rooms);
            _date = date;
        }
    }
}
