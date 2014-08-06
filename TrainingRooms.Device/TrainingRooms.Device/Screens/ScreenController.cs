using System;
using System.Collections.Generic;
using System.Text;
using UpdateControls;
using TrainingRooms.Device.Screens;

namespace TrainingRooms.Device.Screens
{
    public class ScreenController
    {
        private readonly SynchronizationService _synchronizationService;

        public ScreenController()
        {
            _synchronizationService = new SynchronizationService();
            _synchronizationService.Initialize();
        }

        public IScreen MainScreen
        {
            get
            {
                return new RoomSelectorScreen();
            }
        }
    }
}
