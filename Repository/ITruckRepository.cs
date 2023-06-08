using FleetForceAPI.Models;

namespace FleetForceAPI.Repository
{
    public interface ITruckRepository
    {
        Task<Truck> AddTruckAsync(Truck truck);
        Task<Truck> UpdateTruckAsync(Truck truck);

        bool DeleteTruck(string id);
        Task<IEnumerable<Truck>> GetAllTrucksListAsync();
        Truck Details(string id);
    }
}
