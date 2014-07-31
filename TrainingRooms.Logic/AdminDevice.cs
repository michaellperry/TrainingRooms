using System.Collections.Generic;
using TrainingRooms.Logic.Jobs;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence.Strategy;

namespace TrainingRooms.Logic
{
    public class AdminDevice : Device
    {
        private IAsyncJob<Schedule[]> _createSchedules;

        public AdminDevice(IStorageStrategy storage, DateSelectionModel dateSelectionModel)
            : base(storage, dateSelectionModel)
        {
            _createSchedules = Job
                .Observe(() => new ScheduleCreator(
                    VenueToken.Venue.Value.Rooms,
                    dateSelectionModel.SelectedDate))
                .ComputeAsync(async (ScheduleCreator job) =>
                    await job.CreateSchedulesAsync())
                .StartAt(new Schedule[0]);
        }

        public override IEnumerable<Schedule> Schedules
        {
            get { return _createSchedules.Output; }
        }
    }
}
