using AutoMapper;
using FleetForceAPI.DTO;
using FleetForceAPI.Models;

namespace FleetForceAPI.MapModel
{
    public class LoadMapper : Profile
    {
        public LoadMapper()
        {
            // Source ==> Target
            CreateMap<Load, LoadDTO>();
            CreateMap<LoadDTO, Load>();
        }
    }
}
