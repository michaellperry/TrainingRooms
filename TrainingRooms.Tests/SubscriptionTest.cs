using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingRooms.Logic;
using TrainingRooms.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace TrainingRooms.Tests
{
    [TestClass]
    public class SubscriptionTest
    {
        private AdminDevice _admin;
        private SignDevice _sign;

        [TestInitialize]
        public void Initialize()
        {
            var network = new MemoryCommunicationStrategy();
            _admin = new AdminDevice(new MemoryStorageStrategy());
            _sign = new SignDevice(new MemoryStorageStrategy());
            _admin.Community.AddCommunicationStrategy(network);
            _sign.Community.AddCommunicationStrategy(network);
            _admin.Subscribe();
            _sign.Subscribe();
        }

        [TestMethod]
        public async Task SignCanSeeRooms()
        {
            _admin.CreateInstallation();
            _sign.CreateInstallation();

            Venue venueAdmin = await _admin.GetVenueAsync();
            await SynchronizeAsync();
            Venue venueSign = await _sign.GetVenueAsync();

            Room roomA = await venueAdmin.NewRoomAsync();
            roomA.Name = "A";

            await SynchronizeAsync();

            var names = venueSign.Rooms.Select(r => r.Name.Value).ToArray();
            Assert.IsTrue(names.Contains("A"),
                String.Format("No room found with the name A: [{0}]", FormatStringArray(names)));
        }

        private async Task CreateRoom(Device device, string name)
        {
            Venue venue = await device.GetVenueAsync();
            Room roomA = await venue.NewRoomAsync();
            roomA.Name = name;
        }

        private async Task AssertHasRoom(Device device, string name)
        {
            Venue venue = await device.GetVenueAsync();
            var names = venue.Rooms.Select(room => room.Name.Value).ToArray();
            Assert.IsTrue(names.Contains(name),
                String.Format("No room found with the name {0}: [{1}]", name, String.Join(", ", names)));
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
