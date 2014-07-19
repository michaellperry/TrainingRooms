using TrainingRooms.Logic.SelectionModels;
using UpdateControls.Correspondence.Strategy;

namespace TrainingRooms.Logic
{
    public class AdminDevice : Device
    {
        public AdminDevice(IStorageStrategy storage, DateSelectionModel dateSelectionModel)
            : base(storage, dateSelectionModel)
        {
        }
    }
}
