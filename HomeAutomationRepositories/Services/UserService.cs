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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private IUserRepository _userRepo;

        public UserService(IOptions<AuthenticationSettings> authSettings, IUserRepository userRepo)
        {
            _authenticationSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));

            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var shaManaged = new SHA512Managed();
            var encodedEncryptedPassword = Convert.ToBase64String(shaManaged.ComputeHash(Encoding.UTF8.GetBytes(password)));

            var user = await _userRepo.AuthenticateUserAsync(username, encodedEncryptedPassword);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Username.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                }),
                Expires = DateTime.MaxValue,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return new User(user);
        }

        #region Create
        public async Task<User> CreateAsync(User user)
        {
            return new User(
                await _userRepo.CreateUserAsync(new UserEntity(user)));
        }
        #endregion
        #region Read
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var entities = await _userRepo.GetAllAsync();
            var models = entities.Select(x => new User(x));
            return models;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _userRepo.GetByUserNameAsync(userName);
            return new User(user);
        }

        #endregion
    }
}
