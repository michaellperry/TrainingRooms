using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Correspondence;
using TrainingRooms.Model;

namespace TrainingRooms.Model
{
    public partial class Event
    {
        public async Task<Group> GetGroup()
        {
            var eventGroups = await EventGroups.EnsureAsync();
            var firstEventGroup = eventGroups.FirstOrDefault();
            if (firstEventGroup == null)
                return null;

            return await firstEventGroup.Group.EnsureAsync();
        }

        public async Task SetGroup(Group group)
        {
            var prior = await EventGroups.EnsureAsync();
            await Community.AddFactAsync(new EventGroup(this, group, prior));
        }

        public async Task Delete()
        {
            await Community.AddFactAsync(new EventDelete(this));
        }
    }
}
