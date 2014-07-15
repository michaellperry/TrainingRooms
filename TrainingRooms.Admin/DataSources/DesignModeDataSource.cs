using System;
using System.Linq;
using System.Threading.Tasks;
using TrainingRooms.Admin.SelectionModels;
using TrainingRooms.Admin.ViewModels;
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
            _community = new Community(new MemoryStorageStrategy());
            _community.Register<CorrespondenceModel>();
            _installation = await _community.AddFactAsync(new Installation());
            _venue = await _community.AddFactAsync(new Venue());
            _venueToken = await _community.AddFactAsync(new VenueToken(""));
            _venueToken.Venue = _venue;

            _main = await NewMainViewModel();
            _schedule = await NewScheduleViewModel();
            _event = await NewEventViewModel();
        }

        private async Task<MainViewModel> NewMainViewModel()
        {
            await NewSchedule(
                "Training Room A",
                await NewEvent(
                    "Scrum Immersion",
                    new DateTime(2014, 7, 14, 9, 0, 0),
                    new DateTime(2014, 7, 14, 17, 0, 0)),
                await NewEvent(
                    "Ruby Tuesday",
                    new DateTime(2014, 7, 14, 19, 0, 0),
                    new DateTime(2014, 7, 14, 21, 0, 0)));
            await NewSchedule(
                "Training Room B",
                await NewEvent(
                    "C# SIG",
                    new DateTime(2014, 7, 14, 19, 0, 0),
                    new DateTime(2014, 7, 14, 21, 0, 0)));
            await NewSchedule(
                "Training Room C");
            await NewSchedule(
                "Training Room D",
                await NewEvent(
                    "Learn.JS",
                    new DateTime(2014, 7, 14, 19, 0, 0),
                    new DateTime(2014, 7, 14, 21, 0, 0)));
            return new MainViewModel(_community, _installation, _dateSelectionModel, _venueToken);
        }

        private async Task<ScheduleViewModel> NewScheduleViewModel()
        {
            return new ScheduleViewModel(await NewSchedule(
                "Training Room A",
                await NewEvent(
                    "Scrum Immersion",
                    new DateTime(2014, 7, 14, 9, 0, 0),
                    new DateTime(2014, 7, 14, 17, 0, 0)),
                await NewEvent(
                    "Ruby Tuesday",
                    new DateTime(2014, 7, 14, 19, 0, 0),
                    new DateTime(2014, 7, 14, 21, 0, 0))
                ));
        }

        private async Task<EventViewModel> NewEventViewModel()
        {
            return new EventViewModel(await NewEvent(
                "Ruby Tuesday",
                new DateTime(2014, 7, 14, 19, 0, 0),
                new DateTime(2014, 7, 14, 21, 0, 0)));
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

        private async Task<Event> NewEvent(string groupName, DateTime start, DateTime end)
        {
            var day = await _community.AddFactAsync(new Day(start.Date));
            var @event = await _community.AddFactAsync(new Event());
            @event.Start = start;
            @event.End = end;
            var group = await _community.AddFactAsync(new Group(_venue));
            group.Name = groupName;
            @event.Group = group;
            return @event;
        }
    }
}
