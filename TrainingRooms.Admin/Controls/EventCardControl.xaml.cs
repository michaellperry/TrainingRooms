using System.Windows.Controls;
using System.Windows.Input;
using TrainingRooms.Admin.ViewModels;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.Controls
{
    /// <summary>
    /// Interaction logic for EventCardControl.xaml
    /// </summary>
    public partial class EventCardControl : UserControl
    {
        public EventCardControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = ForView.Unwrap<EventViewModel>(DataContext);
            if (viewModel != null)
                viewModel.Edit();
        }
    }
}
