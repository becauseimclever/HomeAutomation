using BecauseImClever.HomeAutomation.AutomationModels;
using BecauseImClever.HomeAutomation.AutomationRepositories;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AutomationRepositories.Tests
{
    public class RoomRepositoryTest
    {
        private readonly Mock<IMongoCollection<Room>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly List<Room> _roomList;
        private readonly Room _roomEntity;
        public RoomRepositoryTest()
        {
            _mockCollection = new Mock<IMongoCollection<Room>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _roomEntity = new Room()
            {
                Id = Guid.NewGuid(),
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
            var repo = new RoomRepository(_mockDatabase.Object);
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
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockDatabase.Object);
            var result = await repo.CreateAsync(_roomEntity).ConfigureAwait(true);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CreateManyReturnsRooms()
        {
            _mockCollection.Setup(x => x.InsertManyAsync(
                It.IsAny<IEnumerable<Room>>(),
                It.IsAny<InsertManyOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Room>)_roomList)));
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockDatabase.Object);
            var result = await repo.CreateManyAsync(_roomList).ConfigureAwait(false);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
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
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);

            var repo = new RoomRepository(_mockDatabase.Object);
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
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockDatabase.Object);
            var result = await repo.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(true);

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
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockDatabase.Object);
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
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockDatabase.Object);
            var result = await repo.DeleteAsync(_roomEntity.Id).ConfigureAwait(true);
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteManyReturnsTrue()
        {
            var ids = _roomList.Select(x => x.Id);
            Mock<DeleteResult> mockResult = new Mock<DeleteResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            mockResult.SetupGet(x => x.DeletedCount).Returns(1);
            _mockCollection.Setup(x => x.DeleteManyAsync(
                It.IsAny<FilterDefinition<Room>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Room>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new RoomRepository(_mockDatabase.Object);
            (bool, long) result = await repo.DeleteManyAsync(ids).ConfigureAwait(true);
            Assert.True(result.Item1);
            Assert.Equal(1, result.Item2);
        }
        #endregion
    }
}