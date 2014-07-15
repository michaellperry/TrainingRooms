using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TrainingRooms.Model;
using UpdateControls.XAML;

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

        public ICommand DeleteRoom
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _schedule.Community.Perform(async delegate
                        {
                            await _schedule.Room.DeleteAsync();
                        });
                    });
            }
        }

        public IEnumerable<EventViewModel> Events
        {
            get
            {
                return
                    from @event in _schedule.Events
                    orderby @event.Start.Value
                    select new EventViewModel(@event);
            }
        }

        public ICommand NewEvent
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _schedule.Community.Perform(async delegate
                        {
                            await _schedule.NewEventAsync();
                        });
                    });
            }
        }
    }
}
