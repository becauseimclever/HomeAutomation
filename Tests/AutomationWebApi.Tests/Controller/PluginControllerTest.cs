using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationWebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AutomationWebApi.Tests.Controller
{
    public class PluginControllerTest
    {
        private readonly Mock<IPluginService> _mockPluignService;
        public PluginControllerTest()
        {
            _mockPluignService = new Mock<IPluginService>();
        }
        [Fact]
        public void CreatePluginController()
        {
            var controller = new PluginController(_mockPluignService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CreatePluginControllerThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { new PluginController(null); });
        }
        [Fact(Skip = "Work in progress")]
        public void GetAllPluginsReturnsPluginsList()
        {
            _mockPluignService.Setup(x => x.GetAll()).Returns(new List<string>());
            var controller = new PluginController(_mockPluignService.Object);
        }
    }
}
