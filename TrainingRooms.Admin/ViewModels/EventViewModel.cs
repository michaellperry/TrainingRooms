using System;
using System.Windows.Input;
using TrainingRooms.Admin.Dialogs;
using TrainingRooms.Admin.EditorModels;
using TrainingRooms.Model;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.ViewModels
{
    public class EventViewModel
    {
        private readonly Event _event;
        private readonly Venue _venue;
        
        public EventViewModel(Event @event, Venue venue)
        {
            _event = @event;
            _venue = venue;
        }

        internal Event Event
        {
            get { return _event; }
        }

        internal Venue Venue
        {
            get { return _venue; }
        }

        public string Time
        {
            get
            {
                var start = DateTime.Today.AddMinutes(_event.StartMinutes.Value);
                var end = DateTime.Today.AddMinutes(_event.EndMinutes.Value);
                return String.Format(@"{0:t} - {1:t}", start, end);
            }
        }

        public string GroupName
        {
            get
            {
                return _event.Group.Value.Name.Value;
            }
        }

        public ICommand DeleteEvent
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _event.Community.Perform(async delegate
                        {
                            await _event.Delete();
                        });
                    });
            }
        }

        public void Edit()
        {
            EventEditorDialog editor = new EventEditorDialog();
            EventEditorModel model = EventEditorModel.FromEvent(Event);
            editor.DataContext = ForView.Wrap(new EventEditorViewModel(model, Venue));
            if (editor.ShowDialog() ?? false)
            {
                model.ToEvent(Event);
            }
        }
    }
}
