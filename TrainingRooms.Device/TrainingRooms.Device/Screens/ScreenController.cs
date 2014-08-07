using System;
using System.Collections.Generic;
using System.Text;
using UpdateControls;
using TrainingRooms.Device.Screens;
using TrainingRooms.Device.Synchronization;
using TrainingRooms.Device.Models;

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
                return new RoomSelectorScreen(_synchronizationService.Device, _roomSelection);
            }
        }
    }
}
