using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TrainingRooms.Admin.Dialogs;
using TrainingRooms.Admin.EditorModels;
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
                    orderby @event.StartMinutes.Value
                    select new EventViewModel(@event, _schedule.Room.Venue);
            }
        }

        public ICommand NewEvent
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        EventEditorDialog editor = new EventEditorDialog();
                        EventEditorModel model = new EventEditorModel();
                        editor.DataContext = ForView.Wrap(new EventEditorViewModel(model, _schedule.Room.Venue));
                        if (editor.ShowDialog() ?? false)
                        {
                            _schedule.Community.Perform(async delegate
                            {
                                    var @event = await _schedule.NewEventAsync();
                                    await model.ToEvent(@event);
                            });
                        }
                    });
            }
        }
    }
}
