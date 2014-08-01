using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class DisplayViewModel : IScreen
    {
        public string RoomName
        {
            get { return "Training Room A"; }
        }

        public string GroupName
        {
            get { return "Papers We Love, Dallas"; }
        }

        public Uri ImageUrl
        {
            get
            {
                return new Uri(
                    "http://qedcode.com/extras/Perry_Headshot_Medium.jpg",
                    UriKind.Absolute);
            }
        }

        public string StartTime
        {
            get { return "7:00 pm"; }
        }

        public string EndTime
        {
            get { return "9:00 pm"; }
        }
    }
}
