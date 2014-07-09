using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        [TestMethod]
        public async Task CanScheduleAnEvent()
        {
            await CreateVenueAsync();

            var room = await _improving.NewRoom();
            var group = await _improving.NewGroup();
            var day = await _community.AddFactAsync(new Day(new DateTime(2014, 7, 9)));
            var roomDay = await _community.AddFactAsync(new Schedule(room, day));
            var upcommingEvent = await _improving.Community.AddFactAsync(
                new Event(roomDay, group));

            var today = await room.ScheduleFor(new DateTime(2014, 7, 9));

            Assert.AreSame(upcommingEvent, today.Events.Single());
        }
	}
}
