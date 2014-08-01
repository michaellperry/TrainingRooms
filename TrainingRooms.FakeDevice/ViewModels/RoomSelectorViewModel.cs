using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UpdateControls.XAML;

namespace TrainingRooms.FakeDevice.ViewModels
{
    public class RoomSelectorViewModel : IScreen
    {
        public IEnumerable<RoomHeader> Rooms
        {
            get
            {
                return new List<RoomHeader>
                {
                    new RoomHeader(),
                    new RoomHeader(),
                    new RoomHeader()
                };
            }
        }

        public RoomHeader SelectedRoom
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        public ICommand Lock
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {

                    });
            }
        }
    }
}
