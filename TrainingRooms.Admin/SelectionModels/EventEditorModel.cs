using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;

namespace TrainingRooms.Admin.SelectionModels
{
    public class EventEditorModel
    {
        private Independent<int> _startMinutes = new Independent<int>();
        private Independent<int> _endMinutes = new Independent<int>();

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
    }
}
