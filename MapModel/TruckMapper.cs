using AutoMapper;
using FleetForceAPI.DTO;
using FleetForceAPI.Models;

namespace FleetForceAPI.MapModel
{
    public class TruckMapper : Profile
    {
        public TruckMapper()
        {
            // Source ==> Target
            CreateMap<Truck, TruckDTO>();
            CreateMap<TruckDTO, Truck>();
        }
    }
}
