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
    public class PluginRepositoryTest
    {
        private readonly Mock<IMongoCollection<Plugin>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly List<Plugin> _pluginList;
        private readonly Plugin _pluginEntity;
        public PluginRepositoryTest()
        {
            _mockCollection = new Mock<IMongoCollection<Plugin>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _pluginEntity = new Plugin()
            {
                Id = Guid.NewGuid(),
                Name = "TestDevice",
                Path = "Plugin/Path"

            };
            _pluginList = new List<Plugin>()
            {
                _pluginEntity
            };
        }
        [Fact]
        public void CreatePluginRepository()
        {
            var repo = new PluginRepository(_mockDatabase.Object);
            Assert.NotNull(repo);
        }
        [Fact]
        public void ConstructorThrowsNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new PluginRepository(null));
        }
        #region Create
        [Fact]
        public async Task CreatePluginReturnsPlugin()
        {
            _mockCollection.Setup(x => x.InsertOneAsync(
                It.IsAny<Plugin>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Plugin>)_pluginList)));
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new PluginRepository(_mockDatabase.Object);
            var result = await repo.CreateAsync(_pluginEntity).ConfigureAwait(true);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CreateManyReturnsPlugins()
        {
            _mockCollection.Setup(x => x.InsertManyAsync(
                It.IsAny<IEnumerable<Plugin>>(),
                It.IsAny<InsertManyOptions>(),
                It.IsAny<CancellationToken>()
                )).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Plugin>)_pluginList)));
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new PluginRepository(_mockDatabase.Object);
            var result = await repo.CreateManyAsync(_pluginList).ConfigureAwait(false);
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
                    It.IsAny<FilterDefinition<Plugin>>(),
                    It.IsAny<FindOptions<Plugin>>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor((ICollection<Plugin>)_pluginList));
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);

            var repo = new PluginRepository(_mockDatabase.Object);
            var result = await repo.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Plugin>>(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetByIdReturnsOne()
        {
            _mockCollection
                .Setup(x => x.FindAsync(
                    It.IsAny<FilterDefinition<Plugin>>(),
                    It.IsAny<FindOptions<Plugin>>(),
                    It.IsAny<CancellationToken>()
                    )).ReturnsAsync(MongoHelper.BuildMockAsyncCursor(_pluginEntity));
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new PluginRepository(_mockDatabase.Object);
            var result = await repo.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(true);

            Assert.IsType<Plugin>(result);
            Assert.Equal(_pluginEntity.Id, result.Id);
        }
        #endregion
        #region Update
        [Fact]
        public async Task UpdateAsyncReturnsTrue()
        {
            Mock<UpdateResult> mockResult = new Mock<UpdateResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<Plugin>>(),
               It.IsAny<UpdateDefinition<Plugin>>(),
               It.IsAny<UpdateOptions>(),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new PluginRepository(_mockDatabase.Object);
            var result = await repo.UpdateAsync(_pluginEntity).ConfigureAwait(true);
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
                It.IsAny<FilterDefinition<Plugin>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new PluginRepository(_mockDatabase.Object);
            var result = await repo.DeleteAsync(_pluginEntity.Id).ConfigureAwait(true);
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteManyReturnsTrue()
        {
            var ids = _pluginList.Select(x => x.Id);
            Mock<DeleteResult> mockResult = new Mock<DeleteResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            mockResult.SetupGet(x => x.DeletedCount).Returns(1);
            _mockCollection.Setup(x => x.DeleteManyAsync(
                It.IsAny<FilterDefinition<Plugin>>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockCollection.Object);
            var repo = new PluginRepository(_mockDatabase.Object);
            (bool, long) result = await repo.DeleteManyAsync(ids).ConfigureAwait(true);
            Assert.True(result.Item1);
            Assert.Equal(1, result.Item2);
        }
        #endregion
    }
}
