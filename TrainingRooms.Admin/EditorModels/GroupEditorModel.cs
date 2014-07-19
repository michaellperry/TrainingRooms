using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingRooms.Model;
using UpdateControls.Fields;

namespace TrainingRooms.Admin.EditorModels
{
    public class GroupEditorModel
    {
        private Independent<string> _name = new Independent<string>();
        private Independent<string> _imageUrl = new Independent<string>();

        public string Name
        {
            get { return _name; }
            set { _name.Value = value; }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl.Value = value; }
        }

        public void ToGroup(Group group)
        {
            group.Name = Name;
            group.ImageUrl = ImageUrl;
        }

        public static GroupEditorModel FromGroup(Group group)
        {
            GroupEditorModel model = new GroupEditorModel();
            model.Name = group.Name;
            model.ImageUrl = group.ImageUrl;
            return model;
        }
    }
}
