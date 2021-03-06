using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using TrainingRooms.Logic;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.Memory;
using UpdateControls.Correspondence.SSCE;
using UpdateControls.Fields;

namespace TrainingRooms.FakeDevice
{
    public class SynchronizationService
    {
        private const string ThisInstallation = "TrainingRooms.FakeDevice.Installation.this";

        private SignDevice _device;
        private DateSelectionModel _dateSelectionModel;

        public void Initialize()
        {
            _dateSelectionModel = new DateSelectionModel();
            string correspondenceDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CorrespondenceApp", "TrainingRooms.FakeDevice", "Correspondence.sdf");
            var storage = new SSCEStorageStrategy(correspondenceDatabase);
            _device = new SignDevice(storage, _dateSelectionModel);

            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);
            _device.Community.AddAsynchronousCommunicationStrategy(communication);

            _device.Subscribe();

            CreateInstallation();

            // Synchronize whenever the user has something to send.
            _device.Community.FactAdded += delegate
            {
                _device.Community.BeginSending();
            };

            // Periodically resume if there is an error.
            DispatcherTimer synchronizeTimer = new DispatcherTimer();
            synchronizeTimer.Tick += delegate
            {
                _device.Community.BeginSending();
                _device.Community.BeginReceiving();
            };
            synchronizeTimer.Interval = TimeSpan.FromSeconds(60.0);
            synchronizeTimer.Start();

            // And synchronize on startup.
            _device.Community.BeginSending();
            _device.Community.BeginReceiving();
        }

        public void InitializeDesignMode()
        {
            _dateSelectionModel = new DateSelectionModel();
            _device = new SignDevice(new MemoryStorageStrategy(), _dateSelectionModel);

            CreateInstallationDesignData();
        }

        public SignDevice Device
        {
            get { return _device; }
        }

        public Community Community
        {
            get { return _device.Community; }
        }

        public Installation Installation
        {
            get { return _device.Installation; }
        }

        private void CreateInstallation()
        {
            _device.CreateInstallation();
        }

        private void CreateInstallationDesignData()
        {
            _device.CreateInstallationDesignData();
        }
    }
}
