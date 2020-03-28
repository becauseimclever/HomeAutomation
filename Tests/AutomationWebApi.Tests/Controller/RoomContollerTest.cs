using AutoFixture;
using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationModels;
using BecauseImClever.HomeAutomation.AutomationWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Tests.Controller
{
    public class RoomControllerTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<IMessageService> _mockMessageService;

        public RoomControllerTest()
        {
            _fixture = new Fixture();
            _mockGroupService = new Mock<IGroupService>();
            _mockMessageService = new Mock<IMessageService>();
        }
        [Fact]
        public void CreateRoomController()
        {
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CreateRoomControllerThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new GroupController(null, null); });
            Assert.Throws<ArgumentNullException>(() => { new GroupController(_mockGroupService.Object, null); });
        }
        [Fact]
        public async Task CreateAsyncReturnsRoom()
        {
            var room = _fixture.Create<Group>();
            _mockGroupService.Setup(x => x.CreateAsync(It.IsAny<Group>())).ReturnsAsync(room);
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            var result = await controller.CreateAsync(room).ConfigureAwait(false);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Group>(okObjectResult.Value);
        }
        [Fact]
        public async Task GetAllAsyncReturnsList()
        {
            var rooms = _fixture.CreateMany<Group>();
            _mockGroupService.Setup(x => x.GetAllAsync()).ReturnsAsync(rooms);
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            var result = await controller.GetAllAsync().ConfigureAwait(false);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsAssignableFrom<IEnumerable<Group>>(okObjectResult.Value);
            Assert.NotEmpty(resultList);
        }
        [Fact]
        public async Task GetByIdAsyncReturnsSingle()
        {
            var room = _fixture.Create<Group>();
            _mockGroupService.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(room);
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            var result = await controller.GetByIdAsync("anyString").ConfigureAwait(false);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Group>(okObjectResult.Value);
        }
        [Fact]
        public async Task UpdateAsyncReturnsBoolean()
        {
            var room = _fixture.Create<Group>();
            _mockGroupService.Setup(x => x.UpdateAsync(It.IsAny<Group>())).ReturnsAsync(true);
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            var result = await controller.UpdateAsync(room).ConfigureAwait(false);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okObjectResult.Value);
        }
        [Fact]
        public async Task DeleteAsyncReturnsNoContent()
        {
            _mockGroupService.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            var result = await controller.DeleteAsync("anyString").ConfigureAwait(false);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteAsyncReturnsBadRequest()
        {
            _mockGroupService.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(false);
            var controller = new GroupController(_mockGroupService.Object, _mockMessageService.Object);
            var result = await controller.DeleteAsync("anyString").ConfigureAwait(false);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
