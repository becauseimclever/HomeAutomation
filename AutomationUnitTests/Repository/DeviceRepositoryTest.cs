using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Repositories.Interface;
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


namespace AutomationUnitTests.Repository
{
    public class DeviceRepositoryTest
    {
        private readonly Mock<IMongoContext<Device>> _mockContext;
        public DeviceRepositoryTest()
        {
            _mockContext = new Mock<IMongoContext<Device>>();
        }

        [Fact]
        public void CreateDeviceRepository()
        {
            var repo = new DeviceRepository(_mockContext.Object);
            Assert.NotNull(repo);
        }
        [Fact]
        public void CreateDeviceRepositoryThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { var repo = new DeviceRepository(null); });
        }
    }
}
