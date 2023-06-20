using FleetForceAPI.Models;

namespace FleetForceAPI.Repository
{
    public interface IDriverRepository
    {
        Task<Driver> AddDriverAsync(Driver driver);
        
        Task<Driver> UpdateDriverAsync(Driver driver);

        bool DeleteDriver(string id);
        
        Task<IEnumerable<Driver>> GetAllDriversListAsync();

        Task<List<Driver>> GetFilterDriversAsync(string filterby);

        Task<List<Driver>> GetSortDriversAsync(string sortOrder);

        Task<List<Driver>> GetPagedDriversAsync(int page, int pageSize);

        Driver Details(string id);

        Task<List<Driver>> GetSearchedDriverAsync(string searchTerm);
    }
}
