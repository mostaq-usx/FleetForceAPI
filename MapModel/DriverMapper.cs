using AutoMapper;
using FleetForceAPI.DTO;
using FleetForceAPI.Models;

namespace FleetForceAPI.MapModel
{
    public class DriverMapper : Profile
    {
        public DriverMapper() 
        {
            // Source ==> Target
            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, Driver>();
        }
    }
}
