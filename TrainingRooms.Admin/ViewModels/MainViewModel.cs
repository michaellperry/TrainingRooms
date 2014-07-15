using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TrainingRooms.Admin.Jobs;
using TrainingRooms.Admin.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.XAML;
using System.Threading.Tasks;

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

        public ICommand NextDate
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _dateSelectionModel.SelectedDate = _dateSelectionModel.SelectedDate.AddDays(1.0);
                    });
            }
        }

        public ICommand PriorDate
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _dateSelectionModel.SelectedDate = _dateSelectionModel.SelectedDate.AddDays(-1.0);
                    });
            }
        }

        public IEnumerable<ScheduleViewModel> Schedules
        {
            get
            {
                return
                    from schedule in _createSchedules.Output
                    orderby schedule.Room.Name.Value
                    select new ScheduleViewModel(schedule);
            }
        }

        public ICommand NewRoom
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _community.Perform(async delegate
                        {
                            var venue = await GetVenueAsync();
                            await venue.NewRoomAsync();
                        });
                    });
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

        private async Task<Venue> GetVenueAsync()
        {
            Venue venue = await _venueToken.Venue.EnsureAsync();
            if (venue.IsNull)
            {
                venue = await _community.AddFactAsync(new Venue());
                _venueToken.Venue = venue;
            }

            return venue;
        }
    }
}
