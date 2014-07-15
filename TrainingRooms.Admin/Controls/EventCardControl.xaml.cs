using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingRooms.Admin.Dialogs;
using TrainingRooms.Admin.SelectionModels;
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
