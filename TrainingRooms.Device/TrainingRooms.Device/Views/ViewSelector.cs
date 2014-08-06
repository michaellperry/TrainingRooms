using System;
using System.Collections.Generic;
using System.Text;
using TrainingRooms.Device.Screens;
using UpdateControls.Fields;
using Xamarin.Forms;

namespace TrainingRooms.Device.Views
{
    public class ViewSelector
    {
        private Dependent<View> _view;
        
        public ViewSelector(ContentPage mainPage, ScreenController screenController)
        {
            _view = new Dependent<View>(() => ResolveView(screenController.MainScreen));
            _view.Subscribe(v => mainPage.Content = v);
        }

        private View ResolveView(IScreen screen)
        {
            return new Label
            {
                Text = "Hello, Correspondence!",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
}
