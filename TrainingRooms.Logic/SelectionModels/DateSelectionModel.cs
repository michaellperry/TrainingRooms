using System;
using UpdateControls.Fields;

namespace TrainingRooms.Logic.SelectionModels
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
