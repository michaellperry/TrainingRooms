using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.EditorModels
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

        public async Task ToEvent(Event @event)
        {
            @event.StartMinutes = StartMinutes;
            @event.EndMinutes = EndMinutes;
            await @event.SetGroup(Group);
        }

        public static async Task<EventEditorModel> FromEvent(Event @event)
        {
            EventEditorModel model = new EventEditorModel();
            model.StartMinutes = await @event.StartMinutes.EnsureAsync();
            model.EndMinutes = await @event.EndMinutes.EnsureAsync();
            var group = await @event.GetGroup();
            model.Group = group;
            return model;
        }
    }
}
