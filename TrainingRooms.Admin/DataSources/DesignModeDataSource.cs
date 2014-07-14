using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingRooms.Admin.ViewModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace TrainingRooms.Admin.DataSources
{
    public class DesignModeDataSource
    {
        private Community _community;
        private Installation _installation;

        public DesignModeDataSource()
        {
            Initialize();
        }

        public MainViewModel Main
        {
            get
            {
                return new MainViewModel(_community, _installation)
                {
                    SeletedDate = new DateTime(2014, 7, 14),
                    Schedules = new List<ScheduleViewModel>
                    {
                        new ScheduleViewModel
                        {
                            RoomName = "Training Room A",
                            Events = new List<EventViewModel>
                            {
                                new EventViewModel
                                {
                                    GroupName = "Srum Immersion",
                                    Start = new DateTime(2014, 7, 14, 9, 0, 0),
                                    End = new DateTime(2014, 7, 14, 17, 0, 0)
                                },
                                new EventViewModel
                                {
                                    GroupName = "Ruby Tuesday",
                                    Start = new DateTime(2014, 7, 14, 19, 0, 0),
                                    End = new DateTime(2014, 7, 14, 21, 0, 0)
                                }
                            }
                        },
                        new ScheduleViewModel
                        {
                            RoomName = "Training Room B",
                            Events = new List<EventViewModel>
                            {
                                new EventViewModel
                                {
                                    GroupName = "C# SIG",
                                    Start = new DateTime(2014, 7, 14, 19, 0, 0),
                                    End = new DateTime(2014, 7, 14, 21, 0, 0)
                                }
                            }
                        },
                        new ScheduleViewModel
                        {
                            RoomName = "Training Room C"
                        },
                        new ScheduleViewModel
                        {
                            RoomName = "Training Room D",
                            Events = new List<EventViewModel>
                            {
                                new EventViewModel
                                {
                                    GroupName = "Learn.JS",
                                    Start = new DateTime(2014, 7, 14, 19, 0, 0),
                                    End = new DateTime(2014, 7, 14, 21, 0, 0)
                                }
                            }
                        },
                    }
                };
            }
        }

        public ScheduleViewModel Schedule
        {
            get
            {
                return new ScheduleViewModel
                {
                    RoomName = "Training Room A",
                    Events = new List<EventViewModel>
                    {
                        new EventViewModel
                        {
                            GroupName = "Srum Immersion",
                            Start = new DateTime(2014, 7, 14, 9, 0, 0),
                            End = new DateTime(2014, 7, 14, 17, 0, 0)
                        },
                        new EventViewModel
                        {
                            GroupName = "Ruby Tuesday",
                            Start = new DateTime(2014, 7, 14, 19, 0, 0),
                            End = new DateTime(2014, 7, 14, 21, 0, 0)
                        }
                    }
                };
            }
        }

        public EventViewModel Event
        {
            get
            {
                return new EventViewModel
                {
                    GroupName = "Ruby Tuesday",
                    Start = new DateTime(2014, 7, 14, 19, 0, 0),
                    End = new DateTime(2014, 7, 14, 21, 0, 0)
                };
            }
        }

        private async Task Initialize()
        {
            _community = new Community(new MemoryStorageStrategy());
            _installation = await _community.AddFactAsync(new Installation());
        }
    }
}
