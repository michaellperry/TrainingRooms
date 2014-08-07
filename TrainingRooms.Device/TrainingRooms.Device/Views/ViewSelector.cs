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
        private Dependent<IScreen> _screen;
        
        public ViewSelector(ContentPage mainPage, ScreenController screenController)
        {
            //mainPage.Content = ResolveView(screenController.MainScreen);
            _screen = new Dependent<IScreen>(() => screenController.MainScreen);
            _screen.OnGet();
            _screen.Subscribe(screen => mainPage.Content = ResolveView(screen));
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
