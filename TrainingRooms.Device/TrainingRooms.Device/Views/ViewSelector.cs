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
            return
                Resolve<RoomSelectorScreen, RoomSelectorView>(screen) ??
                // TODO: Resolve views for additional screeens here.

                new Label
                {
                    Text = String.Format("Cannot resolve view for screen {0}", screen),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
        }

        private View Resolve<TScreen, TView>(IScreen screen)
            where TScreen : class, IScreen
            where TView : View, new()
        {
            var specificScreen = screen as TScreen;
            if (specificScreen == null)
                return null;

            TView view = new TView();
            view.BindingContext = screen;
            return view;
        }
    }
}
