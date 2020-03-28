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

namespace AutomationWebApi.Tests.Controller
{
    public class PluginControllerTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IPluginService> _mockPluginService;
        public PluginControllerTest()
        {
            _fixture = new Fixture();
            _mockPluginService = new Mock<IPluginService>();
        }
        [Fact]
        public void CreatePluginController()
        {
            var controller = new PluginController(_mockPluginService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CreatePluginControllerThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { new PluginController(null); });
        }
        [Fact]
        public async Task GetReturnsList()
        {
            var list = _fixture.CreateMany<Plugin>(10);
            _mockPluginService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var controller = new PluginController(_mockPluginService.Object);
            var pluginList = await controller.GetAsync();
            var okResult = Assert.IsType<OkObjectResult>(pluginList);
            var listResult = Assert.IsAssignableFrom<IEnumerable<Plugin>>(okResult.Value);
            Assert.NotEmpty(listResult);
        }
    }
}
