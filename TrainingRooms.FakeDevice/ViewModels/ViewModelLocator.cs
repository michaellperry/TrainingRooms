using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using UpdateControls.XAML;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignMode)
                _synchronizationService.Initialize();
            else
                _synchronizationService.InitializeDesignMode();
        }

        public object Main
        {
            get
            {
                return ViewModel(() => new MainViewModel(
                    _synchronizationService.Community,
                    _synchronizationService.Installation));
            }
        }
    }
}
