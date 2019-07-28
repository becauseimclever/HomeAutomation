using AutomationFrontEnd.Controllers;
using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Repositories.Interface;
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
    public class RoomsControllerTest
    {
        private readonly Mock<IRoomsService> _mockService;
        public RoomsControllerTest()
        {
            _mockService = new Mock<IRoomsService>();
        }
        [Fact]
        public void CreateRoomController()
        {
            var controller = new RoomsController(_mockService.Object);
            Assert.NotNull(controller);
        }
    }
}
