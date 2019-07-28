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

namespace AutomationUnitTests.Repository
{
    public class RoomRepositoryTest
    {
        private readonly Mock<IMongoCollection<Room>> _mockCollection;
        private readonly Mock<IMongoContext<Room>> _mockContext;
        private readonly List<Room> _roomList;
        private readonly Room _roomEntity;
        public RoomRepositoryTest()
        {
            _mockCollection = new Mock<IMongoCollection<Room>>();
            _mockContext = new Mock<IMongoContext<Room>>();
            _roomEntity = new Room()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "TestDevice",

            };
            _roomList = new List<Room>()
            {
                _roomEntity
            };
        }
        [Fact]
        public void CreateRoomRepository()
        {
            var repo = new RoomRepository(_mockContext.Object);
            Assert.NotNull(repo);
        }
        [Fact]
        public void ConstructorThrowsNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomRepository(null));
        }
        #region Create
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            _mockCollection.Setup(x => x.InsertOneAsync(
                It.IsAny<Room>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Room>)_roomList)));
            _mockContext.Setup(x => x.MongoCollection).Returns(_mockCollection.Object);

            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.CreateRoomAsync(_roomEntity).ConfigureAwait(true);
            Assert.NotNull(result);
        }
        #endregion
        #region Read
        [Fact]
        public async Task GetAllReturnsList()
        {
            _mockCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<Room>>(),
                    It.IsAny<FindOptions<Room>>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor((ICollection<Room>)_roomList));
            _mockContext.Setup(x => x.MongoCollection).Returns(_mockCollection.Object);

            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Room>>(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetByIdReturnsOne()
        {
            _mockCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<Room>>(),
                    It.IsAny<FindOptions<Room>>(),
                    It.IsAny<CancellationToken>()
                    )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor(_roomEntity));
            _mockContext.Setup(x => x.MongoCollection).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.GetByIdAsync(new ObjectId()).ConfigureAwait(true);

            Assert.IsType<Room>(result);
            Assert.Equal(_roomEntity.Id, result.Id);
        }
        #endregion
        #region Update
        [Fact]
        public async Task UpdateAsyncReturnsTrue()
        {
            Mock<UpdateResult> mockResult = new Mock<UpdateResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<Room>>(),
               It.IsAny<UpdateDefinition<Room>>(),
               It.IsAny<UpdateOptions>(),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(mockResult.Object);
            _mockContext.Setup(x => x.MongoCollection).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.UpdateAsync(_roomEntity).ConfigureAwait(true);
            Assert.True(result);
        }
        #endregion
        #region Delete
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            Mock<DeleteResult> mockResult = new Mock<DeleteResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.DeleteOneAsync(
                It.IsAny<FilterDefinition<Room>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockContext.Setup(x => x.MongoCollection).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockContext.Object);
            var result = await repo.DeleteAsync(_roomEntity.Id.ToString()).ConfigureAwait(true);
            Assert.True(result);
        }
        #endregion
    }
}