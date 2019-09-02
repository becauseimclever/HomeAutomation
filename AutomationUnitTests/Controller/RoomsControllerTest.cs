using AutoFixture;
using AutomationAPI.Controllers;
using BecauseImClever.AutomationLogic.Interfaces;
using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace BecauseImClever.AutomationUnitTests.Controller
{
    public class RoomsControllerTest
    {
        private readonly Mock<IRoomService> _mockService;
        private readonly Fixture _fixture;
        public RoomsControllerTest()
        {
            _mockService = new Mock<IRoomService>();
            _fixture = new Fixture();
        }
        [Fact]
        public void CreateRoomController()
        {
            var controller = new RoomController(_mockService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CreateRoomControllerThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var controller = new RoomController(null);
            });
        }
        [Fact]
        public async ValueTask CreateAsyncReturnsRoom()
        {
            var newRoom = _fixture.Create<Room>();

            _mockService.Setup(x => x.CreateAsync(It.IsAny<Room>())).ReturnsAsync(newRoom);

            var controller = new RoomController(_mockService.Object);
            var returnedRoom = await controller.CreateAsync(newRoom);
            Assert.NotNull(returnedRoom);
            var room = returnedRoom as OkObjectResult;
            var roomObject = JsonSerializer.Deserialize<Room>(room.Value.ToString());

            Assert.Equal(newRoom.Id, roomObject.Id);
        }
        [Fact]
        public async ValueTask GetAllAsyncReturnsCollection()
        {
            var roomList = _fixture.Create<IEnumerable<Room>>();
            _mockService.Setup(x => x.GetAllAsync()).ReturnsAsync(roomList);
            var controller = new RoomController(_mockService.Object);
            var result = await controller.GetAllAsync();
            var resultObject = result as OkObjectResult;
            var roomsList = JsonSerializer.Deserialize<IEnumerable<Room>>(resultObject.Value.ToString());
            Assert.NotEmpty(roomsList);
        }
    }
}
