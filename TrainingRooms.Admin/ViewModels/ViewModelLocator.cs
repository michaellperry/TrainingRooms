using TrainingRooms.Logic.SelectionModels;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly DateSelectionModel _dateSelectionModel;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignMode)
                _synchronizationService.Initialize();
            else
                _synchronizationService.InitializeDesignMode();
            _dateSelectionModel = new DateSelectionModel();
        }

        public object Main
        {
            get
            {
                return ViewModel(() => new MainViewModel(
                    _synchronizationService.Community,
                    _synchronizationService.Installation,
                    _dateSelectionModel,
                    _synchronizationService.VenueToken,
                    _synchronizationService.Device));
            }
        }
    }
}
