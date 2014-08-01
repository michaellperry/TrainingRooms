using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TrainingRooms.Logic;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.XAML;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class MainViewModel
    {
        private readonly SignDevice _device;
        private readonly Installation _installation;
        
        public MainViewModel(SignDevice device, Installation installation)
        {
            _device = device;
            _installation = installation;
        }

        public IScreen CurrentView
        {
            get
            {
                if (_device.SelectedRoom.IsNull)
                    return new RoomSelectorViewModel(_device);
                else
                    return new DisplayViewModel(_device.SelectedRoom);
            }
        }

        public bool Synchronizing
        {
            get { return _device.Community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                return _device.Community.LastException == null
                    ? String.Empty
                    : _device.Community.LastException.Message;
            }
        }
    }
}
