using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingRooms.Admin.Jobs;
using TrainingRooms.Admin.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Fields;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.ViewModels
{
    public class MainViewModel
    {
        private readonly Community _community;
        private readonly Installation _installation;
        private readonly DateSelectionModel _dateSelectionModel;
        private readonly VenueToken _venueToken;

        private Dependent<ScheduleCreationJob> _scheduleCreationJobs;
        private Independent<List<Schedule>> _schedules = new Independent<List<Schedule>>(
            new List<Schedule>());
        
        public MainViewModel(
            Community community,
            Installation installation,
            DateSelectionModel dateSelectionModel,
            VenueToken venueToken)
        {
            _community = community;
            _installation = installation;
            _venueToken = venueToken;
            _dateSelectionModel = dateSelectionModel;

            _scheduleCreationJobs = new Dependent<ScheduleCreationJob>(() =>
                new ScheduleCreationJob(_venueToken.Venue.Value.Rooms, _dateSelectionModel.SelectedDate));
        }

        public DateTime SeletedDate
        {
            get { return _dateSelectionModel.SelectedDate; }
            set { _dateSelectionModel.SelectedDate = value; }
        }

        public IEnumerable<ScheduleViewModel> Schedules
        {
            get
            {
                List<Schedule> schedules;
                lock (this)
                {
                    schedules = _schedules.Value;
                }
                return
                    from schedule in schedules
                    select new ScheduleViewModel(schedule);
            }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                return _community.LastException == null
                    ? String.Empty
                    : _community.LastException.Message;
            }
        }
    }
}
