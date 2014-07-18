using System;
using System.IO;
using System.Windows.Threading;
using TrainingRooms.Logic;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.Memory;
using UpdateControls.Correspondence.SSCE;

namespace TrainingRooms.Admin
{
    public class SynchronizationService
    {
        private AdminDevice _device;

        public void Initialize()
        {
            string correspondenceDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CorrespondenceApp", "TrainingRooms.Admin", "Correspondence.sdf");
            var storage = new SSCEStorageStrategy(correspondenceDatabase);
            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);

            _device = new AdminDevice(storage);

            _device.Community.AddAsynchronousCommunicationStrategy(communication);
            _device.Subscribe();

            _device.CreateInstallation();

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
            _device = new AdminDevice(new MemoryStorageStrategy());

            _device.CreateInstallationDesignData();
        }

        public Community Community
        {
            get { return _device.Community; }
        }

        public Installation Installation
        {
            get { return _device.Installation; }
        }

        public VenueToken VenueToken
        {
            get { return _device.VenueToken; }
        }
    }
}
