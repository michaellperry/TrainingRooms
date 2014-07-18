﻿using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Strategy;
using TrainingRooms.Logic;
using TrainingRooms.Model;
using System.Threading.Tasks;

namespace TrainingRooms.Logic
{
    public class AdminDevice : Device
    {
        public AdminDevice(IStorageStrategy storage)
            : base(storage)
        {
        }
    }
}
