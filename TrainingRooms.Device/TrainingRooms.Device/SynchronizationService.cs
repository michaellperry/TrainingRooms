using TrainingRooms.Logic;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace TrainingRooms.Device
{
    public class SynchronizationService
    {
        private SignDevice _device;
        private DateSelectionModel _dateSelectionModel;

        public void Initialize()
        {
            _dateSelectionModel = new DateSelectionModel();
            _device = new SignDevice(new MemoryStorageStrategy(), _dateSelectionModel);

            CreateInstallation();
        }

        public SignDevice Device
        {
            get { return _device; }
        }

        public Community Community
        {
            get { return _device.Community; }
        }

        public Installation Installation
        {
            get { return _device.Installation; }
        }

        private void CreateInstallation()
        {
            _device.CreateInstallation();
        }
    }
}
