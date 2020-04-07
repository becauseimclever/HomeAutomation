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
    public class GroupServiceTest
    {
        private readonly Mock<IGroupRepository> _mockGroupRepo;
        private readonly Fixture fixture;
        public GroupServiceTest()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Register(() => Guid.NewGuid());
            _mockGroupRepo = new Mock<IGroupRepository>();
        }
        [Fact]
        public void ConstuctorThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GroupService(null));
        }
        [Fact]
        public async Task CreateGroupReturnsGroup()
        {
            var room = fixture.Create<Group>();
            room.Id = Guid.NewGuid();
            _mockGroupRepo.Setup(x => x.CreateAsync(It.IsAny<Group>())).ReturnsAsync(room);
            var roomService = new GroupService(_mockGroupRepo.Object);
            var result = await roomService.CreateAsync(room).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Group>(result);
            Assert.Equal(room.Id, result.Id);
            Assert.Equal(room.Name, result.Name);
        }
        [Fact]
        public async Task GetAllReturnsListOfGroups()
        {
            var rooms = fixture.Create<List<Group>>();
            _mockGroupRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(rooms);

            var roomService = new GroupService(_mockGroupRepo.Object);
            var results = await roomService.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Group>>(results);
            Assert.NotEmpty(results);
            Assert.Equal(rooms.Count, results.Count());
        }
        [Fact]
        public async Task GetByIdReturnsGroup()
        {
            var room = fixture.Create<Group>();
            _mockGroupRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(room);

            var roomsService = new GroupService(_mockGroupRepo.Object);
            var result = await roomsService.GetByIdAsync(room.Id.ToString()).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Group>(result);
            Assert.Equal(room.Id, result.Id);
        }
        [Fact]
        public async Task UpdateReturnsTrue()
        {
            var room = fixture.Create<Group>();
            room.Id = Guid.NewGuid();

            _mockGroupRepo.Setup(x => x.UpdateAsync(It.IsAny<Group>())).ReturnsAsync(room);
            var roomService = new GroupService(_mockGroupRepo.Object);
            var result = await roomService.UpdateAsync(room).ConfigureAwait(true);
        }
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            _mockGroupRepo.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            var roomService = new GroupService(_mockGroupRepo.Object);
            var result = await roomService.DeleteAsync(Guid.NewGuid().ToString()).ConfigureAwait(true);

            Assert.True(result);

        }
    }

}
