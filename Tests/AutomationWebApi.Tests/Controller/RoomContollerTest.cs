using AutoFixture;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationWebApi.Controllers;
using Moq;
using System;
using Xunit;

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Tests.Controller
{
    public class RoomContollerTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<RoomService> _mockRoomService;

        public RoomContollerTest()
        {
            _fixture = new Fixture();
            _mockRoomService = new Mock<RoomService>();
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
    }
}
