using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using UpdateControls;
using UpdateControls.Fields;

namespace TrainingRooms.Device.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private List<Dependent> _bindings = new List<Dependent>();

        protected void Bind<T>(Expression<Func<T>> property)
        {
            var accessor = (MemberExpression)property.Body;
            var propertyName = accessor.Member.Name;
            var func = property.Compile();

            var binding = new Dependent<T>(func);
            binding.Subscribe(v => RaisePropertyChanged(propertyName));

            _bindings.Add(binding);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
