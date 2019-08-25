using AutomationAPI.Controllers;
using BecauseImClever.AutomationLogic.Interfaces;
using Moq;
using Xunit;
namespace BecauseImClever.AutomationUnitTests.Controller
{
    public class RoomsControllerTest
    {
        private readonly Mock<IRoomService> _mockService;
        public RoomsControllerTest()
        {
            _mockService = new Mock<IRoomService>();
        }
        [Fact]
        public void CreateRoomController()
        {
            var controller = new RoomController(_mockService.Object);
            Assert.NotNull(controller);
        }
    }
}
