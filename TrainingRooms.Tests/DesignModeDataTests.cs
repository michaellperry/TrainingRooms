using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingRooms.Admin.DataSources;
using System.Linq;
using System.Windows.Input;

namespace TrainingRooms.Tests
{
    [TestClass]
    public class DesignModeDataTests
    {
        [TestMethod]
        public void CanGetEvent()
        {
            CanAccessAllProperties(new DesignModeDataSource().Event);
        }

        [TestMethod]
        public void CanGetSchedule()
        {
            CanAccessAllProperties(new DesignModeDataSource().Schedule);
        }

        [TestMethod]
        public void CanGetMain()
        {
            CanAccessAllProperties(new DesignModeDataSource().Main);
        }

        private void CanAccessAllProperties(object obj)
        {
            var properties = obj.GetType().GetProperties()
                .Select(p => p.GetValue(obj))
                .ToArray();
            var canExecute = properties.OfType<ICommand>()
                .Select(c => c.CanExecute(null))
                .ToArray();
        }
    }
}
