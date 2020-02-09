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
        private readonly Mock<IRoomService> _mockRoomService;

        public RoomControllerTest()
        {
            _fixture = new Fixture();
            _mockRoomService = new Mock<IRoomService>();
        }
        [Fact]
        public void CreateRoomController()
        {
            var controller = new RoomController(_mockRoomService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CreateRoomControllerThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new RoomController(null); });
        }
        [Fact]
        public async Task CreateAsyncReturnsRoom()
        {
            var room = _fixture.Create<Room>();
            _mockRoomService.Setup(x => x.CreateAsync(It.IsAny<Room>())).ReturnsAsync(room);
            var controller = new RoomController(_mockRoomService.Object);
            var result = await controller.CreateAsync(room).ConfigureAwait(false);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Room>(okObjectResult.Value);
        }
        [Fact]
        public async Task GetAllAsyncReturnsList()
        {
            var rooms = _fixture.CreateMany<Room>();
            _mockRoomService.Setup(x => x.GetAllAsync()).ReturnsAsync(rooms);
            var controller = new RoomController(_mockRoomService.Object);
            var result = await controller.GetAllAsync().ConfigureAwait(false);
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsAssignableFrom<IEnumerable<Room>>(okObjectResult.Value);
            Assert.NotEmpty(resultList);
        }
    }
}
