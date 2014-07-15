using System;
using System.Collections.Generic;
using System.Linq;
using TrainingRooms.Admin.Jobs;
using TrainingRooms.Admin.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;

namespace TrainingRooms.Admin.ViewModels
{
    public class MainViewModel
    {
        private readonly Community _community;
        private readonly Installation _installation;
        private readonly DateSelectionModel _dateSelectionModel;
        private readonly VenueToken _venueToken;

        private AsyncJob<ScheduleCreator, Schedule[]> _createSchedules;
        
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

            _createSchedules = new AsyncJob<ScheduleCreator, Schedule[]>(
                new Schedule[0],
                () => new ScheduleCreator(
                    _venueToken.Venue.Value.Rooms,
                    _dateSelectionModel.SelectedDate),
                async (ScheduleCreator job) =>
                    await job.CreateSchedulesAsync());
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
                return
                    from schedule in _createSchedules.Output
                    let name = schedule.Room.Name
                    where name.Candidates.Any()
                    orderby name.Value
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
