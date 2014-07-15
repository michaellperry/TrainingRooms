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
using System.Windows.Shapes;

namespace TrainingRooms.Admin.Dialogs
{
    /// <summary>
    /// Interaction logic for GroupEditorDialog.xaml
    /// </summary>
    public partial class GroupEditorDialog : Window
    {
        public GroupEditorDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.DialogResult = true;
        }
    }
}
