using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories;
using MongoDB.Bson;
using Moq;
using System.Collections.Generic;

namespace AutomationUnitTests
{
    public class RoomServiceTest
    {
        private Mock<IRoomRepository> _mockRepo;
        private readonly List<RoomEntity> _roomList;
        private readonly RoomEntity _roomEntity;
        public RoomServiceTest()
        {
            _roomEntity = new RoomEntity()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "TestDevice",
                Devices = new List<DeviceEntity>()
                      {
                           new DeviceEntity()
                           {
                                Id = ObjectId.GenerateNewId(),
                                 Name="TestDevice"
                           }
                      }
            };
            _roomList = new List<RoomEntity>()
            {
                _roomEntity
            };
        }
        
    }
}
