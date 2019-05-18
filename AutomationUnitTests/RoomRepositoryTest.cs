using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories;
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

namespace AutomationUnitTests
{
    [ExcludeFromCodeCoverage]
    public class RoomRepositoryTest
    {
        private readonly Mock<IMongoCollection<RoomEntity>> _mockCollection;
        private readonly Mock<IMongoContext> _mockContext;
        private readonly List<RoomEntity> _roomList;
        private readonly RoomEntity _roomEntity;
        public RoomRepositoryTest()
        {
            _mockCollection = new Mock<IMongoCollection<RoomEntity>>();
            _mockContext = new Mock<IMongoContext>();
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
        public async Task GetAllReturnsList()
        {
            _mockCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<RoomEntity>>(),
                    It.IsAny<FindOptions<RoomEntity>>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor((ICollection<RoomEntity>)_roomList));
            _mockContext.Setup(x => x.RoomCollection).Returns(_mockCollection.Object);

            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.GetAllAsync();

            Assert.IsType<List<RoomEntity>>(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetByIdReturnsOne()
        {
            _mockCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<RoomEntity>>(),
                    It.IsAny<FindOptions<RoomEntity>>(),
                    It.IsAny<CancellationToken>()
                    )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor(_roomEntity));
            _mockContext.Setup(x => x.RoomCollection).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.GetByIdAsync(new ObjectId());

            Assert.IsType<RoomEntity>(result);
            Assert.Equal(_roomEntity.Id, result.Id);
        }

        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            _mockCollection.Setup(x => x.InsertOneAsync(
                It.IsAny<RoomEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<RoomEntity>)_roomList)));
            _mockContext.Setup(x => x.RoomCollection).Returns(_mockCollection.Object);

            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.CreateRoomAsync(_roomEntity);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task UpdateNameAsyncReturnsTrue()
        {
            Mock<UpdateResult> mockResult = new Mock<UpdateResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<RoomEntity>>(),
               It.IsAny<UpdateDefinition<RoomEntity>>(),
               It.IsAny<UpdateOptions>(),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(mockResult.Object);
            _mockContext.Setup(x => x.RoomCollection).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.UpdateNameAsync(_roomEntity);
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            Mock<DeleteResult> mockResult = new Mock<DeleteResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.DeleteOneAsync(
                It.IsAny<FilterDefinition<RoomEntity>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockContext.Setup(x => x.RoomCollection).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.DeleteAsync(_roomEntity.Id.ToString());
            Assert.True(result);
        }
    }
}