﻿using TrainingRooms.Device.ViewModels;
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

            var roomList = new ListView();
            roomList.SetBinding(ListView.ItemsSourceProperty, new Binding("Rooms"));
            var roomHeaderTemplate = new DataTemplate(() => new Label { Text = "Room" });
            roomList.ItemTemplate = roomHeaderTemplate;
            //Children.Add(roomList);
        }
    }
}