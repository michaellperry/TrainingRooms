using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private MainViewModel _main;
        private ScheduleViewModel _schedule;
        private EventViewModel _event;

        public DesignModeDataSource()
        {
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

            _main = await NewMainViewModel();
            _schedule = await NewScheduleViewModel();
            _event = await NewEventViewModel();
        }

        private async Task<MainViewModel> NewMainViewModel()
        {
            return new MainViewModel(_community, _installation)
            {
                SeletedDate = new DateTime(2014, 7, 14),
                Schedules = new List<ScheduleViewModel>
                {
                    await NewScheduleViewModel(
                        "Training Room A",
                        await NewEvent(
                            "Srum Immersion",
                            new DateTime(2014, 7, 14, 9, 0, 0),
                            new DateTime(2014, 7, 14, 17, 0, 0)),
                        await NewEvent(
                            "Ruby Tuesday",
                            new DateTime(2014, 7, 14, 19, 0, 0),
                            new DateTime(2014, 7, 14, 21, 0, 0))
                    ),
                    await NewScheduleViewModel(
                        "Training Room B",
                        await NewEvent(
                            "C# SIG",
                            new DateTime(2014, 7, 14, 19, 0, 0),
                            new DateTime(2014, 7, 14, 21, 0, 0))
                    ),
                    await NewScheduleViewModel(
                        "Training Room C"
                    ),
                    await NewScheduleViewModel(
                        "Training Room D",
                        await NewEvent(
                            "Learn.JS",
                            new DateTime(2014, 7, 14, 19, 0, 0),
                            new DateTime(2014, 7, 14, 21, 0, 0))
                    )
                }
            };
        }

        private async Task<ScheduleViewModel> NewScheduleViewModel()
        {
            return await NewScheduleViewModel(
                "Training Room A",
                await NewEvent(
                    "Scrum Immersion",
                    new DateTime(2014, 7, 14, 9, 0, 0),
                    new DateTime(2014, 7, 14, 17, 0, 0)),
                await NewEvent(
                    "Ruby Tuesday",
                    new DateTime(2014, 7, 14, 19, 0, 0),
                    new DateTime(2014, 7, 14, 21, 0, 0))
                );
        }

        private async Task<ScheduleViewModel> NewScheduleViewModel(string roomName, params Event[] events)
        {
            var schedule = await NewSchedule(roomName, events);
            return new ScheduleViewModel(schedule);
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
        private async Task<EventViewModel> NewEventViewModel()
        {
            return await NewEventViewModel(
                "Ruby Tuesday",
                new DateTime(2014, 7, 14, 19, 0, 0),
                new DateTime(2014, 7, 14, 21, 0, 0));
        }

        private async Task<EventViewModel> NewEventViewModel(string groupName, DateTime start, DateTime end)
        {
            return new EventViewModel(await NewEvent(groupName, start, end));
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
            var room = await _community.AddFactAsync(new Room(_venue));
            var schedule = await _community.AddFactAsync(new Schedule(room, day));
            await _community.AddFactAsync(new EventSchedule(@event, schedule, Enumerable.Empty<EventSchedule>()));
            return @event;
        }
    }
}
