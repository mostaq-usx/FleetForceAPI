using FleetForceAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FleetForceAPI.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IMongoCollection<Driver> _drivers;
        public DriverRepository(IMongoDatabase mongoDatabase)
        {
            _drivers = mongoDatabase.GetCollection<Driver>("driver");
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

        public async Task<List<Driver>> GetFilterDriversAsync(string filterBy)
        {
            var filter = Builders<Driver>.Filter.Regex(x => x.Name, new BsonRegularExpression(filterBy, "i"));
            var filteredDrivers = await _drivers.Find(filter).ToListAsync();
            return filteredDrivers;
        }

        public async Task<List<Driver>> GetSortDriversAsync(string sortOrder)
        {
            var sortDefinition = Builders<Driver>.Sort.Ascending(p => p.Name);

            if (sortOrder.ToLower() == "desc")
            {
                sortDefinition = Builders<Driver>.Sort.Descending(p => p.Name);
            }

            var sortedDrivers = await _drivers.Find(Builders<Driver>.Filter.Empty)
                                                   .Sort(sortDefinition)
                                                   .ToListAsync();
            return sortedDrivers;
        }

        public async Task<List<Driver>> GetPagedDriversAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var drivers = await _drivers.Find(Builders<Driver>.Filter.Empty)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
            return drivers;
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            var driverDetails = Details(driver.Id);
            if (driverDetails is not null)
            {
                // Automapper or any mapping libraries or function is usefull to map these properties together.
                driverDetails.Name = driver.Name;
                //driverDetails.LoadDate = driver.LoadDate;
                //driverDetails.Customer = driver.Customer;
                //driverDetails.Load = driver.Load;
                await _drivers.ReplaceOneAsync(m => m.Id == driverDetails.Id, driver);
            }
            return driverDetails;
        }

        public async Task<List<Driver>> GetSearchedDriverAsync(string searchTerm)
        {
            var search = Builders<Driver>.Filter.Regex(x => x.Name, new BsonRegularExpression(searchTerm, "i"));
            var searchedDrivers = await _drivers.Find(search).ToListAsync();
            return searchedDrivers;
        }
    }
}
