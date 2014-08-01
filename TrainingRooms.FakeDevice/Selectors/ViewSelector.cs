using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using UpdateControls.XAML;
using TrainingRooms.FakeDevice.ViewModels;
using UpdateControls.XAML.Wrapper;

namespace TrainingRooms.FakeDevice.Selectors
{
    public class ViewSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var wrapper = item as IObjectInstance;
            var element = container as FrameworkElement;
            if (wrapper != null && element != null)
            {
                Type viewModelType = wrapper.WrappedObject.GetType();
                var key = new DataTemplateKey(viewModelType);
                return element.TryFindResource(key) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
