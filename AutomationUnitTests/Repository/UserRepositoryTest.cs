using AutoFixture;
using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests.Repository
{

    public class UserRepositoryTest
    {
        private Fixture fixture;

        private readonly Mock<IMongoCollection<UserEntity>> _mockCollection;
        private readonly Mock<IMongoContext> _mockContext;
        private readonly List<UserEntity> _userList;
        private readonly UserEntity _userEntity;
        public UserRepositoryTest()
        {
            fixture = new Fixture();
            fixture.Register<ObjectId>(() => ObjectId.GenerateNewId());

            _mockCollection = new Mock<IMongoCollection<UserEntity>>();
            _mockContext = new Mock<IMongoContext>();
            _userEntity = fixture.Create<UserEntity>();
            _userList = new List<UserEntity>() { _userEntity };
        }
        [Fact]
        public void ConstructorThrowsNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
        }
        #region Create
        [Fact]
        public async Task CreateUserReturnsUser()
        {
            _mockCollection.Setup(x => x.InsertOneAsync(
                It.IsAny<UserEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<UserEntity>)_userList)));
            _mockContext.Setup(x => x.UserCollection).Returns(_mockCollection.Object);
            var repo = new UserRepository(_mockContext.Object);
            var result = await repo.CreateUserAsync(_userEntity);
            Assert.NotNull(result);
        }
        #endregion
        #region Read
        [Fact]
        public async Task GetAllUsersReturnsList()
        {
            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<UserEntity>>(),
                It.IsAny<FindOptions<UserEntity>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(MongoHelper.BuildMockAsyncCursor((ICollection<UserEntity>)_userList));
            _mockContext.Setup(x => x.UserCollection).Returns(_mockCollection.Object);
            var repo = new UserRepository(_mockContext.Object);
            var result = await repo.GetAllAsync();
            Assert.IsType<List<UserEntity>>(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetByIdAsyncReturnsUserEntity()
        {
            _mockCollection.Setup(x => x.FindAsync(
               It.IsAny<FilterDefinition<UserEntity>>(),
               It.IsAny<FindOptions<UserEntity>>(),
               It.IsAny<CancellationToken>()))
               .ReturnsAsync(MongoHelper.BuildMockAsyncCursor(_userEntity));
            _mockContext.Setup(x => x.UserCollection).Returns(_mockCollection.Object);
            var repo = new UserRepository(_mockContext.Object);
            var result = await repo.GetByIdAsync(_userEntity.Id);
            Assert.IsType<UserEntity>(result);
            Assert.Equal(_userEntity.Id, result.Id);
        }
        [Fact]
        public async Task GetByUserNameAsyncReturnsUserEntity()
        {
            _mockCollection.Setup(x => x.FindAsync(
               It.IsAny<FilterDefinition<UserEntity>>(),
               It.IsAny<FindOptions<UserEntity>>(),
               It.IsAny<CancellationToken>()))
               .ReturnsAsync(MongoHelper.BuildMockAsyncCursor(_userEntity));
            _mockContext.Setup(x => x.UserCollection).Returns(_mockCollection.Object);
            var repo = new UserRepository(_mockContext.Object);
            var result = await repo.GetByUserNameAsync(_userEntity.Username);
            Assert.IsType<UserEntity>(result);
            Assert.Equal(_userEntity.Id, result.Id);
        }
        #endregion
        #region Update
        [Fact]
        public async Task UpdateUserReturnsTrue()
        {
            Mock<UpdateResult> mockResult = new Mock<UpdateResult>();
            mockResult.SetupGet(x => x.IsAcknowledged).Returns(true);
            _mockCollection.Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<UserEntity>>(),
                It.IsAny<UpdateDefinition<UserEntity>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockResult.Object);
            _mockContext.Setup(x => x.UserCollection).Returns(_mockCollection.Object);
            var repo = new UserRepository(_mockContext.Object);
            var result = await repo.UpdateAsync(_userEntity);
            Assert.True(result);
        }
        #endregion

    }
}
