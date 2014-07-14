using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly Schedule _schedule;

        public ScheduleViewModel(Schedule schedule)
        {
            _schedule = schedule;
        }

        public string RoomName
        {
            get { return _schedule.Room.Name.Value; }
            set { _schedule.Room.Name = value; }
        }

        public IEnumerable<EventViewModel> Events
        {
            get
            {
                return
                    from @event in _schedule.Events
                    let start = @event.Start
                    where start.Candidates.Any()
                    orderby start.Value
                    select new EventViewModel(@event);
            }
        }
    }
}
