using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutomationUnitTests.Service
{
    public class DeviceServiceTest
    {
        private readonly Mock<IDeviceRepository> _mockDeviceRepo;
        public DeviceServiceTest()
        {
            _mockDeviceRepo = new Mock<IDeviceRepository>();
        }
        [Fact]
        public void CreateDeviceService()
        {
            var service = new DeviceService(_mockDeviceRepo.Object);
            Assert.NotNull(service);
        }
        [Fact]
        public void CreateDeviceServiceThrowsNewArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { var service = new DeviceService(null); });
        }
    }
}
