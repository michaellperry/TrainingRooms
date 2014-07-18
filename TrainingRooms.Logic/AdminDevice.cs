using System.Text.RegularExpressions;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Strategy;
using UpdateControls.Fields;

namespace TrainingRooms.Logic
{
    public class AdminDevice
    {
        private const string ThisInstallation = "TrainingRooms.Admin.Installation.this";
        private const string TokenIdentifier = "{3721E178-9386-430C-ACF9-E8E058EE653D}";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Independent<Installation> _installation = new Independent<Installation>(
            Installation.GetNullInstance());
        private Independent<VenueToken> _venueToken = new Independent<VenueToken>(
            VenueToken.GetNullInstance());

        public AdminDevice(IStorageStrategy storage)
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

        public void Subscribe()
        {
            _community.Subscribe(() => Installation);
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
    }
}
