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
        private readonly Mock<IMongoCollection<Group>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly List<Group> _groupList;
        private readonly Group _groupEntity;
        public RoomRepositoryTest()
        {
            _mockCollection = new Mock<IMongoCollection<Group>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _groupEntity = new Group()
            {
                Id = Guid.NewGuid(),
                Name = "TestDevice",

            };
            _groupList = new List<Group>()
            {
                _groupEntity
            };
        }
        [Fact]
        public void CreateRoomRepository()
        {
            var repo = new GroupRepository(_mockDatabase.Object);
            Assert.NotNull(repo);
        }
        [Fact]
        public void ConstructorThrowsNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new GroupRepository(null));
        }
        #region Create
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            _mockCollection.Setup(x => x.InsertOneAsync(
                It.IsAny<Group>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Group>)_groupList)));
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new GroupRepository(_mockDatabase.Object);
            var result = await repo.CreateAsync(_groupEntity).ConfigureAwait(true);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CreateManyReturnsRooms()
        {
            _mockCollection.Setup(x => x.InsertManyAsync(
                It.IsAny<IEnumerable<Group>>(),
                It.IsAny<InsertManyOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Group>)_groupList)));
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new GroupRepository(_mockDatabase.Object);
            var result = await repo.CreateManyAsync(_groupList).ConfigureAwait(false);
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
                    It.IsAny<FilterDefinition<Group>>(),
                    It.IsAny<FindOptions<Group>>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor((ICollection<Group>)_groupList));
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);

            var repo = new GroupRepository(_mockDatabase.Object);
            var result = await repo.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Group>>(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetByIdReturnsOne()
        {
            _mockCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<Group>>(),
                    It.IsAny<FindOptions<Group>>(),
                    It.IsAny<CancellationToken>()
                    )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor(_groupEntity));
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new GroupRepository(_mockDatabase.Object);
            var result = await repo.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(true);

            Assert.IsType<Group>(result);
            Assert.Equal(_groupEntity.Id, result.Id);
        }
        #endregion
        #region Update
        [Fact]
        public async Task UpdateAsyncReturnsTrue()
        {
            Mock<UpdateResult> mockResult = new Mock<UpdateResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<Group>>(),
               It.IsAny<UpdateDefinition<Group>>(),
               It.IsAny<UpdateOptions>(),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new GroupRepository(_mockDatabase.Object);
            var result = await repo.UpdateAsync(_groupEntity).ConfigureAwait(true);
            Assert.IsType<Group>(result);
        }
        #endregion
        #region Delete
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            Mock<DeleteResult> mockResult = new Mock<DeleteResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.DeleteOneAsync(
                It.IsAny<FilterDefinition<Group>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new GroupRepository(_mockDatabase.Object);
            var result = await repo.DeleteAsync(_groupEntity.Id).ConfigureAwait(true);
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteManyReturnsTrue()
        {
            var ids = _groupList.Select(x => x.Id);
            Mock<DeleteResult> mockResult = new Mock<DeleteResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            mockResult.SetupGet(x => x.DeletedCount).Returns(1);
            _mockCollection.Setup(x => x.DeleteManyAsync(
                It.IsAny<FilterDefinition<Group>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Group>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new GroupRepository(_mockDatabase.Object);
            (bool, long) result = await repo.DeleteManyAsync(ids).ConfigureAwait(true);
            Assert.True(result.Item1);
            Assert.Equal(1, result.Item2);
        }
        #endregion
    }
}
