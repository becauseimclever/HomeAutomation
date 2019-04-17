using AutoMapper;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomEntity, Room>();
        }
    }
}
