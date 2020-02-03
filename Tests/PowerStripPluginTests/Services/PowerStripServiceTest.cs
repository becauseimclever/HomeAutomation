using BecauseImClever.HomeAutomation.PowerStripPlugin.Services;
using Moq;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PowerStripPluginTests.Services
{
    public class PowerStripServiceTest
    {
        private readonly Mock<IMqttClient> _mockMqttClient;
        public PowerStripServiceTest()
        {
            _mockMqttClient = new Mock<IMqttClient>();
        }
        [Fact]
        public void CreatePowerStripService()
        {
            var service = new PowerStripService(_mockMqttClient.Object);
            Assert.NotNull(service);
            Assert.IsType<PowerStripService>(service);
        }
        [Fact]
        public void CreatePowerStripServiceThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new PowerStripService(null); });
        }
    }
}
