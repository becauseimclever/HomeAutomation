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
using System.Security.Cryptography;

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
        public void ConstructorWithNullOptionsThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, null));
        }
        [Fact]
        public void ConstructorWithNullRepoThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(_mockOptions.Object, null));
        }
        [Fact]
        public async Task AuthenticateUserReturnsUser()
        {
            var shaManaged = new SHA512Managed();
            var testPasswordBytes = shaManaged.ComputeHash(Encoding.UTF8.GetBytes("TestPassword"));
            var testPassword = Convert.ToBase64String(testPasswordBytes);
            var user = _fixture.Create<UserEntity>();
            user.Password = testPassword;
            _mockUserRepo.Setup(x => x.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
            var userService = new UserService(_mockOptions.Object, _mockUserRepo.Object);
            var authenticatedUser = await userService.Authenticate(user.Username, "TestPassword");
            Assert.NotNull(authenticatedUser.Token);
            Assert.Equal(user.Username, authenticatedUser.UserName);
        }
        [Fact]
        public async Task AuthenticateUserReturnsNull()
        {
            UserEntity userEntity = null;
            _mockUserRepo.Setup(x => x.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userEntity);

            var userService = new UserService(_mockOptions.Object, _mockUserRepo.Object);
            var authenticatedUser = await userService.Authenticate(string.Empty, "TestPassword");
            Assert.Null(authenticatedUser);
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
            Assert.IsAssignableFrom<IEnumerable<User>>(result);
            Assert.NotNull(result);
            Assert.All(result, x => Assert.NotNull(x.Id));
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
