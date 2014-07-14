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
                    new ScheduleViewModel
                    {
                        RoomName = "Training Room A",
                        Events = new List<EventViewModel>
                        {
                            await NewEventViewModel(
                                "Srum Immersion",
                                new DateTime(2014, 7, 14, 9, 0, 0),
                                new DateTime(2014, 7, 14, 17, 0, 0)),
                            await NewEventViewModel(
                                "Ruby Tuesday",
                                new DateTime(2014, 7, 14, 19, 0, 0),
                                new DateTime(2014, 7, 14, 21, 0, 0))
                        }
                    },
                    new ScheduleViewModel
                    {
                        RoomName = "Training Room B",
                        Events = new List<EventViewModel>
                        {
                            await NewEventViewModel(
                                "C# SIG",
                                new DateTime(2014, 7, 14, 19, 0, 0),
                                new DateTime(2014, 7, 14, 21, 0, 0))
                        }
                    },
                    new ScheduleViewModel
                    {
                        RoomName = "Training Room C"
                    },
                    new ScheduleViewModel
                    {
                        RoomName = "Training Room D",
                        Events = new List<EventViewModel>
                        {
                            await NewEventViewModel(
                                "Learn.JS",
                                new DateTime(2014, 7, 14, 19, 0, 0),
                                new DateTime(2014, 7, 14, 21, 0, 0))
                        }
                    },
                }
            };
        }

        private async Task<ScheduleViewModel> NewScheduleViewModel()
        {
            return new ScheduleViewModel
            {
                RoomName = "Training Room A",
                Events = new List<EventViewModel>
                {
                    await NewEventViewModel(
                        "Srum Immersion",
                        new DateTime(2014, 7, 14, 9, 0, 0),
                        new DateTime(2014, 7, 14, 17, 0, 0)),
                    await NewEventViewModel(
                        "Ruby Tuesday",
                        new DateTime(2014, 7, 14, 19, 0, 0),
                        new DateTime(2014, 7, 14, 21, 0, 0))
                }
            };
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
            return new EventViewModel(@event);
        }
    }
}
