using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace TrainingRooms.Tests
{
    [TestClass]
    public class ModelTest
    {
        private Community _community;
        private Venue _improving;

        [TestInitialize]
        public void Initialize()
        {
            var sharedCommunication = new MemoryCommunicationStrategy();
            _community = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
				;
        }

        private async Task CreateVenueAsync()
        {
            _improving = await _community.AddFactAsync(
                new Venue());
        }

        [TestMethod]
        public async Task CanCreateARoom()
        {
            await CreateVenueAsync();

            var room = await _improving.NewRoom();

            Assert.AreSame(room, _improving.Rooms.Single());
        }

        [TestMethod]
        public async Task CanNameARoom()
        {
            await CreateVenueAsync();

            var room = await _improving.NewRoom();
            room.Name = "A";

            Assert.AreEqual("A", room.Name.Value);
        }
	}
}
