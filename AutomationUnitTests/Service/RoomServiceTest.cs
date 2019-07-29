using AutoFixture;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests.Service
{
    public class RoomsServiceTest
    {
        private readonly Mock<IRoomRepository> _mockRoomRepo;
        private readonly Mock<IDeviceRepository> _mockDeviceRepo;
        private Fixture fixture;
        public RoomsServiceTest()
        {
            fixture = new Fixture();
            fixture.Register(() => ObjectId.GenerateNewId());
            fixture.Register<Device>(() => new PowerStrip());
            _mockRoomRepo = new Mock<IRoomRepository>();
            _mockDeviceRepo = new Mock<IDeviceRepository>();
        }
        [Fact]
        public void ConstuctorThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomsService(null, null));
            Assert.Throws<ArgumentNullException>(() => new RoomsService(_mockRoomRepo.Object, null));
        }
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            var room = fixture.Create<Room>();
            room.Id = ObjectId.GenerateNewId();
            _mockRoomRepo.Setup(x => x.CreateRoomAsync(It.IsAny<Room>())).ReturnsAsync(room);
            var roomService = new RoomsService(_mockRoomRepo.Object, _mockDeviceRepo.Object);
            var result = await roomService.CreateAsync(room).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Room>(result);
            Assert.Equal(room.Id, result.Id);
            Assert.Equal(room.Name, result.Name);
        }
        [Fact]
        public async Task GetAllReturnsListOfRooms()
        {
            var rooms = fixture.Create<List<Room>>();
            _mockRoomRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(rooms);

            var roomService = new RoomsService(_mockRoomRepo.Object, _mockDeviceRepo.Object);
            var results = await roomService.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Room>>(results);
            Assert.NotEmpty(results);
            Assert.Equal(rooms.Count, results.Count);
        }
        [Fact]
        public async Task GetByIdReturnsRoom()
        {
            var room = fixture.Create<Room>();
            _mockRoomRepo.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(room);

            var roomsService = new RoomsService(_mockRoomRepo.Object, _mockDeviceRepo.Object);
            var result = await roomsService.GetByIdAsync(room.Id.ToString()).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Room>(result);
            Assert.Equal(room.Id, result.Id);
        }
        [Fact]
        public async Task UpdateReturnsTrue()
        {
            var room = fixture.Create<Room>();
            room.Id = ObjectId.GenerateNewId();

            _mockRoomRepo.Setup(x => x.UpdateAsync(It.IsAny<Room>())).ReturnsAsync(true);
            var roomService = new RoomsService(_mockRoomRepo.Object, _mockDeviceRepo.Object);
            var result = await roomService.UpdateAsync(room).ConfigureAwait(true);
        }
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            _mockRoomRepo.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);

            var roomService = new RoomsService(_mockRoomRepo.Object, _mockDeviceRepo.Object);
            var result = await roomService.DeleteAsync("IDSTRING").ConfigureAwait(true);

            Assert.True(result);

        }
    }
}
