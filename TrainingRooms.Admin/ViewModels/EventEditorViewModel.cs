using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingRooms.Admin.SelectionModels;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.ViewModels
{
    public class EventEditorViewModel
    {
        private readonly EventEditorModel _event;

        public EventEditorViewModel(EventEditorModel @event)
        {
            _event = @event;
        }

        public string Start
        {
            get
            {
                DateTime time = DateTime.Today.AddMinutes(_event.StartMinutes);
                return String.Format("{0:t}", time);
            }
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                {
                    _event.StartMinutes = (int)time.TimeOfDay.TotalMinutes;
                }
            }
        }

        public string End
        {
            get
            {
                DateTime time = DateTime.Today.AddMinutes(_event.EndMinutes);
                return String.Format("{0:t}", time);
            }
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                {
                    _event.EndMinutes = (int)time.TimeOfDay.TotalMinutes;
                }
            }
        }
    }
}
