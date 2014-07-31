using System.Collections.Generic;
using System.Linq;
using TrainingRooms.Logic.Jobs;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence.Strategy;
using UpdateControls.Fields;

namespace TrainingRooms.Logic
{
    public class SignDevice : Device
    {
        private Independent<Room> _selectedRoom = new Independent<Room>(
            Room.GetNullInstance());
        private IAsyncJob<Schedule[]> _createSchedules;

        public SignDevice(IStorageStrategy storage, DateSelectionModel dateSelectionModel)
            : base(storage, dateSelectionModel)
        {
            _createSchedules = Job
                .Observe(() => new ScheduleCreator(
                    new List<Room> { _selectedRoom.Value }
                        .Where(r => !r.IsNull),
                    dateSelectionModel.SelectedDate))
                .ComputeAsync(async (ScheduleCreator job) =>
                    await job.CreateSchedulesAsync())
                .StartAt(new Schedule[0]);
        }

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom.Value = value; }
        }

        public override IEnumerable<Schedule> Schedules
        {
            get { return _createSchedules.Output; }
        }
    }
}
