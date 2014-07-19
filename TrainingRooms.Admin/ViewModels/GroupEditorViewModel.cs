using TrainingRooms.Admin.EditorModels;

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
