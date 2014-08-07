using TrainingRooms.Device.Screens;
using TrainingRooms.Device.ViewModels;
using Xamarin.Forms;

namespace TrainingRooms.Device.Views
{
    public class RoomSelectorView : StackLayout
    {
        public RoomSelectorView()
        {
            var statusLabel = new Label();
            statusLabel.SetBinding<RoomSelectorScreen>(
                Label.TextProperty, s => s.Status);
            Children.Add(statusLabel);

            var roomLabel = new Label();
            roomLabel.SetBinding<RoomSelectorScreen>(
                Label.TextProperty, s => s.Selection);
            Children.Add(roomLabel);

            var roomList = new ListView();
            roomList.SetBinding<RoomSelectorScreen>(
                ListView.ItemsSourceProperty, s => s.Rooms);
            roomList.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new TextCell();
                cell.SetBinding<RoomHeader>(TextCell.TextProperty, h => h.Name);
                return cell;
            });
            roomList.SetBinding<RoomSelectorScreen>(
                ListView.SelectedItemProperty, s => s.SelectedRoom);
            Children.Add(roomList);
        }
    }
}
