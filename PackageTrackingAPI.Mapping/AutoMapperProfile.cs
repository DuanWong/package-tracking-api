using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserCreateDto>().ReverseMap();

            CreateMap<Package, PackageDto>().ReverseMap();

            CreateMap<TrackingEvent, TrackingEventDto>().ReverseMap();

            CreateMap<Alert, AlertDto>().ReverseMap();
        }
    }
}
