using HomeAutomationRepositories.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests
{
    public class RoomRepositoryTest
    {
        private List<RoomEntity> RoomList;
        public RoomRepositoryTest()
        {
            RoomList = new List<RoomEntity>()
            {
                new RoomEntity()
                {
                     Id = ObjectId.GenerateNewId(),
                      Name="TestDevice",
                      Devices = new List<DeviceEntity>()
                      {
                           new DeviceEntity()
                           {
                                Id = ObjectId.GenerateNewId(),
                                 Name="TestDevice"
                           }
                      }
                }
            };
        }

        [Fact]
        public async Task GetAllReturnsList()
        {


        }
    }
}
