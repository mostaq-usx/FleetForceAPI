using FleetForceAPI.Models;

namespace FleetForceAPI.Repository
{
    public interface ITruckRepository
    {
        Task<Truck> AddTruckAsync(Truck truck);

        Task<Truck> UpdateTruckAsync(Truck truck);

        bool DeleteTruck(string id);
        
        Task<IEnumerable<Truck>> GetAllTrucksListAsync();
        
        Task<List<Truck>> GetFilterTrucksAsync(string filterby);
        
        Task<List<Truck>> GetSortTrucksAsync(string sortOrder);

        Task<List<Truck>> GetPagedTrucksAsync(int page, int pageSize);

        Truck Details(string id);

        Task<List<Truck>> GetSearchedTruckAsync(string searchTerm);
    }
}
