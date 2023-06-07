using FleetForceAPI.Models;
using MongoDB.Driver;

namespace FleetForceAPI.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IMongoCollection<Driver> _drivers;
        public DriverRepository(IMongoDatabase mongoDatabase)
        {
            _drivers = mongoDatabase.GetCollection<Driver>("my_col");
        }
        public async Task<Driver> AddDriverAsync(Driver driver)
        {
            if (driver is not null)
            {
                driver.Id = Guid.NewGuid().ToString();
                await _drivers.InsertOneAsync(driver);
            }
            return driver;
        }

        public bool DeleteDriver(string id)
        {
            var driver = Details(id);
            if (driver is not null)
            {
                _drivers.DeleteOne(m => m.Id == id);
                return true;
            }
            return false;
        }

        public Driver Details(string id)
        {
            return _drivers.Find(m => m.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Driver>> GetAllDriversListAsync()
        {
            await Task.Delay(100);

            return await _drivers.Find(_ => true).ToListAsync();
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            var driverDetails = Details(driver.Id);
            if (driverDetails is not null)
            {
                // Automapper or any mapping libraries or function is usefull to map these properties together.
                driverDetails.Name = driver.Name;
                driverDetails.LoadDate = driver.LoadDate;
                driverDetails.Customer = driver.Customer;
                driverDetails.Load = driver.Load;
                await _drivers.ReplaceOneAsync(m => m.Id == driverDetails.Id, driver);
            }
            return driverDetails;
        }
    }
}
