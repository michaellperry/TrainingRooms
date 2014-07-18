using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Strategy;

namespace TrainingRooms.Logic
{
    public class SignDevice : Device
    {
        public SignDevice(IStorageStrategy storage)
            : base(storage)
        {
        }
    }
}
