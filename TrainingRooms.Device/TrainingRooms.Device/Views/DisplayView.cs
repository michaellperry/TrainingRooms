using System;
using System.Linq.Expressions;
using TrainingRooms.Device.Screens;
using Xamarin.Forms;

namespace TrainingRooms.Device.Views
{
    public class DisplayView : StackLayout
    {
        public DisplayView()
        {
            LabelFor(s => s.RoomName);
            LabelFor(s => s.GroupName);
            ImageFor(s => s.ImageUrl);
            LabelFor(s => s.StartTime);
            LabelFor(s => s.EndTime);
        }

        private Label LabelFor(Expression<Func<DisplayScreen, object>> text)
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, text);
            Children.Add(label);
            return label;
        }

        private Image ImageFor(Expression<Func<DisplayScreen, object>> source)
        {
            var image = new Image();
            image.SetBinding<DisplayScreen>(Image.SourceProperty, source);
            Children.Add(image);
            return image;
        }
    }
}
