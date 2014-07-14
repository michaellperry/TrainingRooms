using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainingRooms.Admin.ViewModels
{
    public class EventViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Time
        {
            get
            {
                return String.Format(@"{0:t} - {1:t}", Start, End);
            }
        }
        public string GroupName { get; set; }
    }
}
