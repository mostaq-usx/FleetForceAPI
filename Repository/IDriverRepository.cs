using FleetForceAPI.Models;

namespace FleetForceAPI.Repository
{
    public interface IDriverRepository
    {
        Task<Driver> AddDriver(Driver driver);
        Task<Driver> UpdateDriver(Driver driver);

        bool DeleteDriver(string id);
        IEnumerable<Driver> List();
        Driver Details(string id);
    }
}
