using AutoMapper;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<DeviceEntity, Device>()
                .Include<PowerStripEntity, PowerStrip>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<PowerStripEntity, PowerStrip>();
        }
    }
}
