using AutoFixture;
using AutoFixture.AutoMoq;
using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AutomationLogic.Tests
{
    public class RoomsServiceTest
    {
        private readonly Mock<IRoomRepository> _mockRoomRepo;
        private Fixture fixture;
        public RoomsServiceTest()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Register(() => Guid.NewGuid());
            _mockRoomRepo = new Mock<IRoomRepository>();
        }
        [Fact]
        public void ConstuctorThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomService(null));
        }
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            var room = fixture.Create<Room>();
            room.Id = Guid.NewGuid();
            _mockRoomRepo.Setup(x => x.CreateAsync(It.IsAny<Room>())).ReturnsAsync(room);
            var roomService = new RoomService(_mockRoomRepo.Object);
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

            var roomService = new RoomService(_mockRoomRepo.Object);
            var results = await roomService.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Room>>(results);
            Assert.NotEmpty(results);
            Assert.Equal(rooms.Count, results.Count());
        }
        [Fact]
        public async Task GetByIdReturnsRoom()
        {
            var room = fixture.Create<Room>();
            _mockRoomRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(room);

            var roomsService = new RoomService(_mockRoomRepo.Object);
            var result = await roomsService.GetByIdAsync(room.Id.ToString()).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Room>(result);
            Assert.Equal(room.Id, result.Id);
        }
        [Fact]
        public async Task UpdateReturnsTrue()
        {
            var room = fixture.Create<Room>();
            room.Id = Guid.NewGuid();

            _mockRoomRepo.Setup(x => x.UpdateAsync(It.IsAny<Room>())).ReturnsAsync(true);
            var roomService = new RoomService(_mockRoomRepo.Object);
            var result = await roomService.UpdateAsync(room).ConfigureAwait(true);
        }
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            _mockRoomRepo.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            var roomService = new RoomService(_mockRoomRepo.Object);
            var result = await roomService.DeleteAsync(Guid.NewGuid().ToString()).ConfigureAwait(true);

            Assert.True(result);

        }
    }

}
