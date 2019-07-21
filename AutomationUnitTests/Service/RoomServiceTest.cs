using AutoFixture;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories.Interface;
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
        private readonly Mock<IRoomRepository> _mockRepo;
        private Fixture fixture;
        public RoomsServiceTest()
        {
            fixture = new Fixture();
            fixture.Register(() => ObjectId.GenerateNewId());
            fixture.Register<DeviceEntity>(() => new PowerStripEntity());
            _mockRepo = new Mock<IRoomRepository>();
        }
        [Fact]
        public void ConstuctorThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomsService(null));
        }
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            var room = fixture.Create<Room>();
            room.Id = ObjectId.GenerateNewId();
            _mockRepo.Setup(x => x.CreateRoomAsync(It.IsAny<Room>())).ReturnsAsync(room);
            var roomService = new RoomsService(_mockRepo.Object);
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
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(rooms);

            var roomService = new RoomsService(_mockRepo.Object);
            var results = await roomService.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Room>>(results);
            Assert.NotEmpty(results);
            Assert.Equal(rooms.Count, results.Count);
        }
        [Fact]
        public async Task GetByIdReturnsRoom()
        {
            var room = fixture.Create<Room>();
            _mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(room);

            var roomsService = new RoomsService(_mockRepo.Object);
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

            _mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Room>())).ReturnsAsync(true);
            var roomService = new RoomsService(_mockRepo.Object);
            var result = await roomService.UpdateAsync(room).ConfigureAwait(true);
        }
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            _mockRepo.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);

            var roomService = new RoomsService(_mockRepo.Object);
            var result = await roomService.DeleteAsync("IDSTRING").ConfigureAwait(true);

            Assert.True(result);

        }
    }
}
