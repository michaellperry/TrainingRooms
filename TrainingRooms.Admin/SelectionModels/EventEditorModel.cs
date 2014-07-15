using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.SelectionModels
{
    public class EventEditorModel
    {
        private Independent<int> _startMinutes = new Independent<int>(9 * 60);
        private Independent<int> _endMinutes = new Independent<int>(17 * 60);
        private Independent<Group> _group = new Independent<Group>(
            Group.GetNullInstance());

        public int StartMinutes
        {
            get { return _startMinutes; }
            set { _startMinutes.Value = value; }
        }

        public int EndMinutes
        {
            get { return _endMinutes; }
            set { _endMinutes.Value = value; }
        }

        public Group Group
        {
            get { return _group; }
            set { _group.Value = value; }
        }

        public void ToEvent(Event @event)
        {
            @event.StartMinutes = StartMinutes;
            @event.EndMinutes = EndMinutes;
            @event.Group = Group;
        }

        public static EventEditorModel FromEvent(Event @event)
        {
            EventEditorModel model = new EventEditorModel();
            model.StartMinutes = @event.StartMinutes;
            model.EndMinutes = @event.EndMinutes;
            model.Group = @event.Group;
            return model;
        }
    }
}
