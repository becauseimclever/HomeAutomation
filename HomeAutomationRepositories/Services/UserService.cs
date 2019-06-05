using HomeAutomationRepositories.Authentication;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Repositories.Interface;
using HomeAutomationRepositories.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private IEnumerable<User> _users;
        private IUserRepository _userRepo;

        public UserService(IOptions<AuthenticationSettings> authSettings, IUserRepository userRepo)
        {
            _authenticationSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));

            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public Task<User> Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString())
                }),
                Expires = DateTime.MaxValue,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return Task.FromResult(user);
        }

        #region Create
        public async Task<User> CreateAsync(User user)
        {
            return UserEntity.ConvertToModel(
                await _userRepo.CreateUserAsync(User.ConvertToEntity(user)));
        }
        #endregion
        #region Read
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var entities = await _userRepo.GetAllAsync();
            var models = entities.Select(x => UserEntity.ConvertToModel(x));
            return models;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _userRepo.GetByUserNameAsync(userName);
            return UserEntity.ConvertToModel(user);
        }

        #endregion
    }
}
