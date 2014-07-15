using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRooms.Model
{
    public partial class Group
    {
        public async Task DeleteAsync()
        {
            await Community.AddFactAsync(new GroupDelete(this));
        }
    }
}
