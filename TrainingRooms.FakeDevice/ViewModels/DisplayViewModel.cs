using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingRooms.Logic.Jobs;
using TrainingRooms.Model;
using UpdateControls.Fields;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class DisplayViewModel : IScreen
    {
        private readonly Room _room;

        private Independent<DateTime> _today = new Independent<DateTime>(
            DateTime.Today);
        private Independent<int> _minutesOfDay = new Independent<int>(
            (int)DateTime.Now.TimeOfDay.TotalMinutes);

        private Timer _timer;

        private IAsyncJob<Schedule> _todaysSchedule;
        private IAsyncJob<Schedule> _tomorrowsSchedule;
        private Dependent<Event> _nextEvent;

        public DisplayViewModel(Room room)
        {
            _room = room;

            _todaysSchedule = Job
                .Observe(() => Today)
                .ComputeAsync(async date => await _room.ScheduleForAsync(date))
                .StartAt(Schedule.GetNullInstance());
            _tomorrowsSchedule = Job
                .Observe(() => Today.AddDays(1))
                .ComputeAsync(async date => await _room.ScheduleForAsync(date))
                .StartAt(Schedule.GetNullInstance());
            _nextEvent = new Dependent<Event>(GetNextEvent);

            TimeSpan fiveMinutes = TimeSpan.FromMinutes(5.0);
            _timer = new Timer(EveryFiveMinutes, null, fiveMinutes, fiveMinutes);
        }

        public string RoomName
        {
            get { return _room.Name; }
        }

        public string GroupName
        {
            get
            {
                Event nextEvent = _nextEvent.Value;
                if (nextEvent.IsNull)
                    return "Open";

                return nextEvent.EventGroups
                    .Select(eg => eg.Group == null ? null : eg.Group.Name.Value)
                    .FirstOrDefault();
            }
        }

        public Uri ImageUrl
        {
            get
            {
                Event nextEvent = _nextEvent.Value;
                if (nextEvent.IsNull)
                    return null;

                string groupUrl = nextEvent.EventGroups
                    .Select(eg => eg.Group == null ? null : eg.Group.ImageUrl.Value)
                    .FirstOrDefault();
                return groupUrl == null
                    ? null
                    : new Uri(groupUrl, UriKind.Absolute);
            }
        }

        public string StartTime
        {
            get
            {
                Event nextEvent = _nextEvent.Value;
                if (nextEvent.IsNull)
                    return string.Empty;

                return string.Format("{0:t}", TimeSpan.FromMinutes(
                    nextEvent.StartMinutes.Value));
            }
        }

        public string EndTime
        {
            get
            {
                Event nextEvent = _nextEvent.Value;
                if (nextEvent.IsNull)
                    return string.Empty;

                return string.Format("{0:t}", TimeSpan.FromMinutes(
                    nextEvent.EndMinutes.Value));
            }
        }

        private DateTime Today
        {
            get
            {
                lock (this)
                {
                    return _today.Value;
                }
            }
            set
            {
                lock (this)
                {
                    _today.Value = value;
                }
            }
        }

        private int MinutesOfDay
        {
            get
            {
                lock (this)
                {
                    return _minutesOfDay.Value;
                }
            }
            set
            {
                lock (this)
                {
                    _minutesOfDay.Value = value;
                }
            }
        }

        private void EveryFiveMinutes(object state)
        {
            Today = DateTime.Today;
            MinutesOfDay = (int)DateTime.Now.TimeOfDay.TotalMinutes;
        }

        private Event GetNextEvent()
        {
            var todaysEvents = _todaysSchedule.Output.Events;
            var tomorrowsEvents = _tomorrowsSchedule.Output.Events;
            var allEvents = todaysEvents.Concat(tomorrowsEvents);

            var nextEvent = allEvents
                //.Where(e => e.StartMinutes.Value > MinutesOfDay)
                .OrderBy(e => e.StartMinutes.Value)
                .FirstOrDefault() ?? Event.GetNullInstance();

            return nextEvent;
        }
    }
}
