using System;
using System.Linq;
using System.Threading.Tasks;
using TrainingRooms.Admin.ViewModels;
using TrainingRooms.Logic;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace TrainingRooms.Admin.DataSources
{
    public class DesignModeDataSource
    {
        private Community _community;
        private Installation _installation;
        private Venue _venue;
        private VenueToken _venueToken;
        private AdminDevice _device;

        private DateSelectionModel _dateSelectionModel;

        private MainViewModel _main;
        private ScheduleViewModel _schedule;
        private EventViewModel _event;

        public DesignModeDataSource()
        {
            _dateSelectionModel = new DateSelectionModel();
            _dateSelectionModel.SelectedDate = new DateTime(2014, 7, 14);

            Initialize().Wait();
        }

        public MainViewModel Main
        {
            get { return _main; }
        }

        public ScheduleViewModel Schedule
        {
            get { return _schedule; }
        }

        public EventViewModel Event
        {
            get { return _event; }
        }

        private async Task Initialize()
        {
            _device = new AdminDevice(new MemoryStorageStrategy(), new DateSelectionModel());
            _device.CreateInstallationDesignData();
            _community = _device.Community;
            _installation = _device.Installation;
            _venue = await _community.AddFactAsync(new Venue());
            _venueToken = _device.VenueToken;
            _venueToken.Venue = _venue;

            _main = await NewMainViewModel();
            _schedule = await NewScheduleViewModel();
            _event = await NewEventViewModel();
        }

        private async Task<MainViewModel> NewMainViewModel()
        {
            var date = new DateTime(2014, 7, 14);

            await NewSchedule(
                "Training Room A",
                await NewEvent(
                    "Scrum Immersion",
                    date,
                    9 * 60,
                    17 * 60),
                await NewEvent(
                    "Ruby Tuesday",
                    date,
                    19 * 60,
                    21 * 60));
            await NewSchedule(
                "Training Room B",
                await NewEvent(
                    "C# SIG",
                    date,
                    19 * 60,
                    21 * 60));
            await NewSchedule(
                "Training Room C");
            await NewSchedule(
                "Training Room D",
                await NewEvent(
                    "Learn.JS",
                    date,
                    19 * 60,
                    21 * 60));
            return new MainViewModel(_community, _installation, _dateSelectionModel, _venueToken, _device);
        }

        private async Task<ScheduleViewModel> NewScheduleViewModel()
        {
            var date = new DateTime(2014, 7, 14);

            return new ScheduleViewModel(await NewSchedule(
                "Training Room A",
                await NewEvent(
                    "Scrum Immersion",
                    date,
                    9 * 60,
                    17 * 60),
                await NewEvent(
                    "Ruby Tuesday",
                    date,
                    19 * 60,
                    21 * 60)
                ));
        }

        private async Task<EventViewModel> NewEventViewModel()
        {
            var date = new DateTime(2014, 7, 14);

            return new EventViewModel(await NewEvent(
                "Ruby Tuesday",
                date,
                19 * 60,
                21 * 60), _venue);
        }

        private async Task<Schedule> NewSchedule(string roomName, params Event[] events)
        {
            var room = await _community.AddFactAsync(new Room(_venue));
            room.Name = roomName;
            var day = await _community.AddFactAsync(new Day(new DateTime(2014, 7, 14)));
            var schedule = await _community.AddFactAsync(new Schedule(room, day));

            foreach (var @event in events)
            {
                await _community.AddFactAsync(new EventSchedule(
                    @event,
                    schedule,
                    Enumerable.Empty<EventSchedule>()));
            }
            return schedule;
        }

        private async Task<Event> NewEvent(string groupName, DateTime date, int startMinutes, int endMinutes)
        {
            var day = await _community.AddFactAsync(new Day(date.Date));
            var @event = await _community.AddFactAsync(new Event());
            @event.StartMinutes = startMinutes;
            @event.EndMinutes = endMinutes;
            var group = await _community.AddFactAsync(new Group(_venue));
            group.Name = groupName;
            @event.Group = group;
            return @event;
        }
    }
}
