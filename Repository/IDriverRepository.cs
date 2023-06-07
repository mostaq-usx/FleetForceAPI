using FleetForceAPI.Models;

namespace FleetForceAPI.Repository
{
    public interface IDriverRepository
    {
        Task<Driver> AddDriverAsync(Driver driver);
        Task<Driver> UpdateDriverAsync(Driver driver);

        bool DeleteDriver(string id);
        Task<IEnumerable<Driver>> GetAllDriversListAsync();
        Driver Details(string id);
    }
}
