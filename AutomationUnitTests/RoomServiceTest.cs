using AutoMapper;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Profiles;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Services;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests
{
    public class RoomServiceTest
    {
        private Mock<IRoomRepository> _mockRepo;
        private IMapper _mapper;
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
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeviceProfile());
                cfg.AddProfile(new RoomProfile());
            });
            _mapper = mapConfig.CreateMapper();
            var service = new RoomsService(_mockRepo.Object, _mapper);
            var response = await service.Create()
        }
    }
}
