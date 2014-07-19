using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrainingRooms.Logic;
using TrainingRooms.Logic.SelectionModels;
using TrainingRooms.Model;
using UpdateControls.Correspondence.Memory;

namespace TrainingRooms.Tests
{
    [TestClass]
    public class SubscriptionTest
    {
        private AdminDevice _admin;
        private SignDevice _sign;

        private Venue _venueAdmin;
        private Venue _venueSign;

        [TestInitialize]
        public void Initialize()
        {
            var network = new MemoryCommunicationStrategy();
            _admin = new AdminDevice(new MemoryStorageStrategy(), new DateSelectionModel()
                {
                    SelectedDate = new DateTime(2014, 7, 18)
                });
            _sign = new SignDevice(new MemoryStorageStrategy(), new DateSelectionModel()
                {
                    SelectedDate = new DateTime(2014, 7, 18)
                });
            _admin.Community.AddCommunicationStrategy(network);
            _sign.Community.AddCommunicationStrategy(network);
            _admin.Subscribe();
            _sign.Subscribe();
        }

        [TestMethod]
        public async Task SignCanSeeRooms()
        {
            await InitializeVenuesAsync();

            await CreateRoomAsync(_venueAdmin, "A");
            await SynchronizeAsync();
            AssertHasRoom(_venueSign, "A");
        }

        [TestMethod]
        public async Task SignCanSeeEventsInSelectedRoom()
        {
            await InitializeVenuesAsync();

            Room roomAdmin = await CreateRoomAsync(_venueAdmin, "A");
            await CreateRoomAsync(_venueAdmin, "B");
            var scheduleAdmin = await roomAdmin.ScheduleForAsync(new DateTime(2014, 7, 18));
            var @event = await scheduleAdmin.NewEventAsync();
            await SynchronizeAsync();

            _sign.SelectedRoom = _venueSign.Rooms.Where(r => r.Name == "A").Single();
            await SynchronizeAsync();
            var scheduleSign = await _venueSign.Rooms.Where(r => r.Name == "A").Single()
                .ScheduleForAsync(new DateTime(2014, 7, 18));
            Assert.AreEqual(1, scheduleSign.Events.Count());
        }

        [TestMethod]
        public async Task SignCannotSeeEventsInUnselectedRoom()
        {
            await InitializeVenuesAsync();

            Room roomAdmin = await CreateRoomAsync(_venueAdmin, "A");
            await CreateRoomAsync(_venueAdmin, "B");
            var scheduleAdmin = await roomAdmin.ScheduleForAsync(new DateTime(2014, 7, 18));
            var @event = await scheduleAdmin.NewEventAsync();
            await SynchronizeAsync();

            _sign.SelectedRoom = _venueSign.Rooms.Where(r => r.Name == "B").Single();
            await SynchronizeAsync();
            var scheduleSign = await _venueSign.Rooms.Where(r => r.Name == "A").Single()
                .ScheduleForAsync(new DateTime(2014, 7, 18));
            Assert.AreEqual(0, scheduleSign.Events.Count());
        }

        private async Task InitializeVenuesAsync()
        {
            _admin.CreateInstallation();
            _sign.CreateInstallation();

            _venueAdmin = await _admin.GetVenueAsync();
            await SynchronizeAsync();
            _venueSign = await _sign.GetVenueAsync();
        }

        private async Task<Room> CreateRoomAsync(Venue venue, string name)
        {
            Room room = await venue.NewRoomAsync();
            room.Name = name;
            return room;
        }

        private void AssertHasRoom(Venue venue, string name)
        {
            var names = venue.Rooms.Select(r => r.Name.Value).ToArray();
            Assert.IsTrue(names.Contains(name),
                String.Format("No room found with the name {0}: [{1}]", name, FormatStringArray(names)));
        }

        private async Task SynchronizeAsync()
        {
            while ((await _admin.Community.SynchronizeAsync()) && (await _sign.Community.SynchronizeAsync())) ;
        }

        private static string FormatStringArray(string[] names)
        {
            return String.Join(", ", names.Select(str => "\"" + str + "\"").ToArray());
        }
    }
}
