using TrainingRooms.Device.Models;
using TrainingRooms.Device.Synchronization;

namespace TrainingRooms.Device.Screens
{
    public class ScreenController
    {
        private readonly SynchronizationService _synchronizationService;

        private RoomSelection _roomSelection;

        public ScreenController()
        {
            _synchronizationService = new SynchronizationService();
            _synchronizationService.Initialize();

            _roomSelection = new RoomSelection();
        }

        public IScreen MainScreen
        {
            get
            {
                var device = _synchronizationService.Device;
                var selectedRoom = device.SelectedRoom;
                return selectedRoom.IsNull
                    ? (IScreen)new RoomSelectorScreen(device, _roomSelection)
                    : (IScreen)new DisplayScreen(selectedRoom);
            }
        }
    }
}
