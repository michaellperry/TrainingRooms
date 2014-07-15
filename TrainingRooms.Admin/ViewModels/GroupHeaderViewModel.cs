using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingRooms.Model;

namespace TrainingRooms.Admin.ViewModels
{
    public class GroupHeaderViewModel
    {
        private readonly Group _group;

        public GroupHeaderViewModel(Group group)
        {
            _group = group;            
        }

        internal Group Group
        {
            get { return _group; }
        }

        public string Name
        {
            get { return _group.Name; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            var that = obj as GroupHeaderViewModel;
            if (that == null)
                return false;

            return Object.Equals(this._group, that._group);
        }

        public override int GetHashCode()
        {
            return _group.GetHashCode();
        }
    }
}
