﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using TrainingRooms.Device.ViewModels;
using TrainingRooms.Logic;
using UpdateControls.Fields;
using System.Linq;

namespace TrainingRooms.Device.Screens
{
    public class RoomSelectorScreen : ViewModelBase, IScreen
    {
        private readonly SignDevice _device;

        public RoomSelectorScreen(SignDevice device)
        {
            _device = device;

            Bind(() => Status);
            //Bind(() => Rooms);
        }

        public string Status
        {
            get
            {
                if (_device.Community.Synchronizing)
                    return "Synchronizing...";

                var lastException = _device.Community.LastException;
                if (lastException != null)
                    return _device.Community.LastException.Message;

                return "Ready";
            }
        }

        public List<string> Rooms
        {
            get
            {
                return
                    (from room in _device.VenueToken.Venue.Value.Rooms
                     select room.Name.Value)
                    .ToList();
            }
        }
    }
}
