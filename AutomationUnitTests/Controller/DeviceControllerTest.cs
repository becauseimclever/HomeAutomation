using AutomationFrontEnd.Controllers;
using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests.Controller
{
    public class DeviceControllerTest
    {
        private readonly Mock<IDeviceService> _mockService;
        public DeviceControllerTest()
        {
            _mockService = new Mock<IDeviceService>();
        }
        [Fact]
        public void CreateDeviceController()
        {
            var controller = new DeviceController(_mockService.Object);
            Assert.NotNull(controller);
        }
        [Fact]
        public void CreateDeviceControllerThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { var controller = new DeviceController(null); });
        }
    }
}
