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

            var room = await _improving.NewRoomAsync();

            Assert.AreSame(room, _improving.Rooms.Single());
        }

        [TestMethod]
        public async Task CanNameARoom()
        {
            await CreateVenueAsync();

            var room = await _improving.NewRoomAsync();
            room.Name = "A";

            Assert.AreEqual("A", room.Name.Value);
        }

        [TestMethod]
        public async Task CanScheduleAnEvent()
        {
            await CreateVenueAsync();

            var room = await _improving.NewRoomAsync();
            var group = await _improving.NewGroupAsync();
            var schedule = await room.ScheduleForAsync(new DateTime(2014, 7, 9));
            var @event = await schedule.NewEventAsync();
            @event.Group = group;
            @event.StartMinutes = 18 * 60;
            @event.EndMinutes = 21 * 60;

            Assert.AreSame(@event, schedule.Events.Single());
            Assert.AreSame(group, @event.Group.Value);
            Assert.AreEqual(18 * 60, @event.StartMinutes.Value);
            Assert.AreEqual(21 * 60, @event.EndMinutes.Value);
        }
	}
}
