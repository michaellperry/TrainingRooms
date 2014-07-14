using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainingRooms.Admin.ViewModels
{
    public class ScheduleViewModel
    {
        public string RoomName { get; set; }
        public List<EventViewModel> Events { get; set; }
    }
}
