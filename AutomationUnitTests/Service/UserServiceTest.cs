using AutoFixture;
using HomeAutomationRepositories.Authentication;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Repositories.Interface;
using HomeAutomationRepositories.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests.Service
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IOptions<AuthenticationSettings>> _mockOptions;
        private Fixture _fixture;
        public UserServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Register<ObjectId>(() => ObjectId.GenerateNewId());

            _mockUserRepo = new Mock<IUserRepository>();
            var settings = new AuthenticationSettings()
            {
                Secret = "01189998819991197253"
            };
            _mockOptions = new Mock<IOptions<AuthenticationSettings>>();
            _mockOptions.Setup(x => x.Value).Returns(settings);

        }
        [Fact]
        public void ConstructorThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, null));
        }
        #region Create
        [Fact]
        public async Task CreateUserReturnsUserAsync()
        {
            var user = _fixture.Create<User>();
            user.Id = ObjectId.GenerateNewId().ToString();
            _mockUserRepo.Setup(x => x.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(User.ConvertToEntity(user));

            var userService = new UserService(_mockOptions.Object, _mockUserRepo.Object);
            var result = await userService.CreateAsync(user);
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Token, result.Token);
        }
        #endregion
        #region Read
        [Fact]
        public async Task GetAllReturnsUserList()
        {
            var users = _fixture.CreateMany<UserEntity>();
            _mockUserRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
            var userService = new UserService(_mockOptions.Object, _mockUserRepo.Object);
            var result = await userService.GetAllAsync();
        }
        [Fact]
        public async Task GetByUserNameReturnsUser()
        {
            var user = _fixture.Create<UserEntity>();
            _mockUserRepo.Setup(x => x.GetByUserNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            var userService = new UserService(_mockOptions.Object, _mockUserRepo.Object);
            var result = await userService.GetByUserNameAsync(user.Username);

            Assert.Equal(user.Username, result.UserName);
        }
        #endregion

    }
}
