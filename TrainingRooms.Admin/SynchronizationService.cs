using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.Memory;
using UpdateControls.Correspondence.SSCE;
using UpdateControls.Fields;

namespace TrainingRooms.Admin
{
    public class SynchronizationService
    {
        private const string ThisInstallation = "TrainingRooms.Admin.Installation.this";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Independent<Installation> _installation = new Independent<Installation>(
            Installation.GetNullInstance());

        public void Initialize()
        {
            string correspondenceDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CorrespondenceApp", "TrainingRooms.Admin", "Correspondence.sdf");
            var storage = new SSCEStorageStrategy(correspondenceDatabase);
            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);

            _community = new Community(storage);
            _community.AddAsynchronousCommunicationStrategy(communication);
            _community.Register<CorrespondenceModel>();
            _community.Subscribe(() => Installation);

            CreateInstallation();

            // Synchronize whenever the user has something to send.
            _community.FactAdded += delegate
            {
                _community.BeginSending();
            };

            // Periodically resume if there is an error.
            DispatcherTimer synchronizeTimer = new DispatcherTimer();
            synchronizeTimer.Tick += delegate
            {
                _community.BeginSending();
                _community.BeginReceiving();
            };
            synchronizeTimer.Interval = TimeSpan.FromSeconds(60.0);
            synchronizeTimer.Start();

            // And synchronize on startup.
            _community.BeginSending();
            _community.BeginReceiving();
        }

        public void InitializeDesignMode()
        {
            _community = new Community(new MemoryStorageStrategy());
            _community.Register<CorrespondenceModel>();

            CreateInstallationDesignData();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Installation Installation
        {
            get
            {
                lock (this)
                {
                    return _installation;
                }
            }
            private set
            {
                lock (this)
                {
                    _installation.Value = value;
                }
            }
        }

        private void CreateInstallation()
        {
			_community.Perform(async delegate
			{
				var installation = await _community.LoadFactAsync<Installation>(ThisInstallation);
				if (installation == null)
				{
					installation = await _community.AddFactAsync(new Installation());
					await _community.SetFactAsync(ThisInstallation, installation);
				}
				Installation = installation;
			});
        }

        private void CreateInstallationDesignData()
        {
			_community.Perform(async delegate
			{
				var individual = await _community.AddFactAsync(new Installation());
				Installation = individual;
			});
        }
    }
}
