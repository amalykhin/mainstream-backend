using AutoMapper;
using SteamingService.Entities;
using SteamingService.Models;

namespace SteamingService.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<StartStreamModel, Stream>();
        }
    }
}