using Xamarin.Forms;

namespace TrainingRooms.Device.Views
{
    public class RoomSelectorView : StackLayout
    {
        public RoomSelectorView()
        {
            var statusLabel = new Label();
            statusLabel.SetBinding(Label.TextProperty, new Binding("Status"));
            Children.Add(statusLabel);
        }
    }
}
