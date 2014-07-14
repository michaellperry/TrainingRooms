using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.ViewModels
{
    public class EventViewModel
    {
        private readonly Event _event;

        public EventViewModel(Event @event)
        {
            _event = @event;
        }

        public string Time
        {
            get
            {
                return String.Format(@"{0:t} - {1:t}", _event.Start.Value, _event.End.Value);
            }
        }

        public string GroupName
        {
            get
            {
                return _event.Group.Value.Name.Value;
            }
        }
    }
}
