using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;

namespace TrainingRooms.Admin.SelectionModels
{
    public class DateSelectionModel
    {
        private Independent<DateTime> _selectedDate = new Independent<DateTime>(DateTime.Today);

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate.Value = value; }
        }
    }
}
