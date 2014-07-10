using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.XAML;

namespace TrainingRooms.Admin.ViewModels
{
    public class MainViewModel
    {
        private readonly Community _community;
        private readonly Installation _installation;
        
        public MainViewModel(Community community, Installation installation)
        {
            _community = community;
            _installation = installation;
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                return _community.LastException == null
                    ? String.Empty
                    : _community.LastException.Message;
            }
        }
    }
}
