using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrainingRooms.Logic.Jobs;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Strategy;
using UpdateControls.Fields;

namespace TrainingRooms.Logic
{
    public abstract class Device
    {
        private const string ThisInstallation = "TrainingRooms.Admin.Installation.this";
        private const string TokenIdentifier = "{3721E178-9386-430C-ACF9-E8E058EE653D}";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Independent<Installation> _installation = new Independent<Installation>(
            Installation.GetNullInstance());
        private Independent<VenueToken> _venueToken = new Independent<VenueToken>(
            VenueToken.GetNullInstance());

        public Device(IStorageStrategy storage, DateSelectionModel dateSelectionModel)
        {
            _community = new Community(storage);
            _community.Register<CorrespondenceModel>();
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

        public VenueToken VenueToken
        {
            get
            {
                lock (this)
                {
                    return _venueToken;
                }
            }
            set
            {
                lock (this)
                {
                    _venueToken.Value = value;
                }
            }
        }

        public abstract IEnumerable<Schedule> Schedules
        {
            get;
        }

        public void CreateInstallation()
        {
            Community.Perform(async delegate
            {
                var installation = await Community.LoadFactAsync<Installation>(ThisInstallation);
                if (installation == null)
                {
                    installation = await Community.AddFactAsync(new Installation());
                    await Community.SetFactAsync(ThisInstallation, installation);
                }
                Installation = installation;

                VenueToken = await Community.AddFactAsync(
                    new VenueToken(TokenIdentifier));
            });
        }

        public void CreateInstallationDesignData()
        {
            Community.Perform(async delegate
            {
                var individual = await Community.AddFactAsync(new Installation());
                Installation = individual;

                VenueToken = await Community.AddFactAsync(
                    new VenueToken(TokenIdentifier));
            });
        }

        public void Subscribe()
        {
            Community.Subscribe(() => Installation);
            Community.Subscribe(() => VenueToken);
            Community.Subscribe(() => VenueToken.Venue.Value);
            Community.Subscribe(() => Schedules);
            Community.Subscribe(() => Schedules.SelectMany(s => s.Events));
        }

        public async Task<Venue> GetVenueAsync()
        {
            Venue venue = await VenueToken.Venue.EnsureAsync();
            if (venue.IsNull)
            {
                venue = await Community.AddFactAsync(new Venue());
                VenueToken.Venue = venue;
            }
            return venue;
        }
    }
}
