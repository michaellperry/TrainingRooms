using TrainingRooms.Logic.SelectionModels;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;

        public ViewModelLocator()
        {
            ForView.Initialize();

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
                    _synchronizationService.Installation,
                    _synchronizationService.DateSelectionModel,
                    _synchronizationService.Device));
            }
        }
    }
}
