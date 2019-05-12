using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Repositories;

namespace HomeAutomationRepositories.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserClaimRepository _claimRepo;
        private readonly IMapper _mapper;
        public AuthorizationService(IUserClaimRepository userClaimRepository, IMapper mapper)
        {
            _mapper = mapper;
            _claimRepo = userClaimRepository;
        }
        public async Task<UserClaim> Create(UserClaim userIdentity)
        {
            UserClaimEntity claimEntity = (UserClaimEntity)_mapper.Map(userIdentity, typeof(UserClaim), typeof(UserClaimEntity));
            var newClaim = await _claimRepo.Create(claimEntity);
            return ConvertEntitytoModel(newClaim);
        }

        public Task<bool> Delete(string macAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserClaim>> GetAll()
        {
            var claims = await _claimRepo.GetAll();
            var claimsList = new List<UserClaim>();
            foreach (var claim in claims)
            {
                claimsList.Add(ConvertEntitytoModel(claim));
            }
            return claimsList;
        }

        public Task<UserClaim> GetByMacAddress(string macAddress)
        {
            throw new NotImplementedException();
        }

        public Task<UserClaim> Update(UserClaim userIdentity)
        {
            throw new NotImplementedException();
        }
        private UserClaim ConvertEntitytoModel(UserClaimEntity entity)
        {
            var userClaim = new UserClaim()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Groups = entity.Groups,
                macAddress = entity.macAddress
            };
            return userClaim;
        }
    }
}
