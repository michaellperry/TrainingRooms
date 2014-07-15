using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrainingRooms.Admin.Dialogs;
using TrainingRooms.Admin.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.ViewModels
{
    public class EventEditorViewModel
    {
        private readonly EventEditorModel _event;
        private readonly Venue _venue;
        
        public EventEditorViewModel(EventEditorModel @event, Venue venue)
        {
            _event = @event;
            _venue = venue;
        }

        public string Start
        {
            get
            {
                DateTime time = DateTime.Today.AddMinutes(_event.StartMinutes);
                return String.Format("{0:t}", time);
            }
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                {
                    _event.StartMinutes = (int)time.TimeOfDay.TotalMinutes;
                }
            }
        }

        public string End
        {
            get
            {
                DateTime time = DateTime.Today.AddMinutes(_event.EndMinutes);
                return String.Format("{0:t}", time);
            }
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                {
                    _event.EndMinutes = (int)time.TimeOfDay.TotalMinutes;
                }
            }
        }

        public IEnumerable<GroupHeaderViewModel> Groups
        {
            get
            {
                return
                    from @group in _venue.Groups
                    orderby @group.Name.Value
                    select new GroupHeaderViewModel(@group);
            }
        }

        public GroupHeaderViewModel SelectedGroup
        {
            get
            {
                if (_event.Group.IsNull)
                    return null;
                else
                    return new GroupHeaderViewModel(_event.Group);
            }
            set
            {
                if (value == null)
                    _event.Group = Group.GetNullInstance();
                else
                    _event.Group = value.Group;
            }
        }

        public ICommand NewGroup
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        GroupEditorDialog dialog = new GroupEditorDialog();
                        GroupEditorModel model = new GroupEditorModel();
                        dialog.DataContext = ForView.Wrap(new GroupEditorViewModel(model));
                        if (dialog.ShowDialog() ?? false)
                        {
                            _venue.Community.Perform(async delegate
                            {
                                var group = await _venue.NewGroupAsync();
                                model.ToGroup(group);
                                _event.Group = group;
                            });
                        }
                    });
            }
        }

        public ICommand EditGroup
        {
            get
            {
                return MakeCommand
                    .When(() => !_event.Group.IsNull)
                    .Do(delegate
                    {
                        Group group = _event.Group;
                        GroupEditorDialog dialog = new GroupEditorDialog();
                        GroupEditorModel model = GroupEditorModel.FromGroup(group);
                        dialog.DataContext = ForView.Wrap(new GroupEditorViewModel(model));
                        if (dialog.ShowDialog() ?? false)
                        {
                            model.ToGroup(group);
                        }
                    });
            }
        }

        public ICommand DeleteGroup
        {
            get
            {
                return MakeCommand
                    .When(() => !_event.Group.IsNull)
                    .Do(delegate
                    {
                        if (MessageBox.Show(
                            String.Format("Delete the {0} group?", _event.Group.Name.Value),
                            "Delete Group",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            _venue.Community.Perform(async delegate
                            {
                                await _event.Group.DeleteAsync();
                            });
                        }
                    });
            }
        }
    }
}
