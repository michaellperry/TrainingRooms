using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingRooms.Device.Screens;
using TrainingRooms.Device.Views;
using UpdateControls;
using Xamarin.Forms;

namespace TrainingRooms.Device
{
	public class App
	{
        private static ViewSelector _viewSelector;

        public static Page GetMainPage(IDispatchOnUIThread dispatchAdapter)
        {
            UpdateScheduler.Initialize(a => dispatchAdapter.Invoke(a));

            var mainPage = new ContentPage();
            var screenController = new ScreenController();
            _viewSelector = new ViewSelector(mainPage, screenController);
            return mainPage;
        }
	}
}
