using AutoMapper;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Profiles
{
    public class UserClaimProfile : Profile
    {
        public UserClaimProfile()
        {
            CreateMap<UserClaimEntity, UserClaim>();
        }
    }
}
