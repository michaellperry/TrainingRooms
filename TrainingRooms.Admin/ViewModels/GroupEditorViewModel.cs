using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingRooms.Admin.SelectionModels;

namespace TrainingRooms.Admin.ViewModels
{
    public class GroupEditorViewModel
    {
        private readonly GroupEditorModel _model;

        public GroupEditorViewModel(GroupEditorModel model)
        {
            _model = model;            
        }

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; }
        }

        public string ImageUrl
        {
            get { return _model.ImageUrl; }
            set { _model.ImageUrl = value; }
        }
    }
}
