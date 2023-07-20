using FleetForceAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;

namespace FleetForceAPI.Repository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly IMongoCollection<Truck> _trucks;
        public TruckRepository(IMongoDatabase mongoDatabase)
        {
            _trucks = mongoDatabase.GetCollection<Truck>("truck");
        }
        public async Task<Truck> AddTruckAsync(Truck truck)
        {
            if (truck is not null)
            {
                truck.Id = Guid.NewGuid().ToString();
                await _trucks.InsertOneAsync(truck);
            }
            return truck;
        }

        public bool DeleteTruck(string id)
        {
            var truck = Details(id);
            if (truck is not null)
            {
                _trucks.DeleteOne(m => m.Id == id);
                return true;
            }
            return false;
        }

        public Truck Details(string id)
        {
            return _trucks.Find(m => m.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Truck>> GetAllTrucksListAsync()
        {
            await Task.Delay(100);

            return await _trucks.Find(_ => true).ToListAsync();
        }

        public async Task<List<Truck>> GetFilterTrucksAsync(string filterBy)
        {
            var filter = Builders<Truck>.Filter.Regex(x => x.Number, new BsonRegularExpression(filterBy, "i"));
            var filteredTrucks = await _trucks.Find(filter).ToListAsync();
            return filteredTrucks;
        }

        public async Task<List<Truck>> GetSortTrucksAsync(string sortOrder)
        {
            var sortDefinition = Builders<Truck>.Sort.Ascending(p => p.Number);

            if (sortOrder.ToLower() == "desc")
            {
                sortDefinition = Builders<Truck>.Sort.Descending(p => p.Number);
            }

            var sortedTrucks = await _trucks.Find(Builders<Truck>.Filter.Empty)
                                                   .Sort(sortDefinition)
                                                   .ToListAsync();
            return sortedTrucks;
        }

        public async Task<List<Truck>> GetPagedTrucksAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var trucks = await _trucks.Find(Builders<Truck>.Filter.Empty)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
            return trucks;
        }

        public async Task<Truck> UpdateTruckAsync(Truck truck)
        {
            var truckDetails = Details(truck.Id);
            if (truckDetails is not null)
            {
                // Automapper or any mapping libraries or function is usefull to map these properties together.
                truckDetails.Number = truck.Number;
                truckDetails.Driver = truck.Driver;
                truckDetails.Availability = truck.Availability;
                truckDetails.Loads = truck.Loads;
                
                
                //truckDetails.CoveringManager = truck.CoveringManager;
                //truckDetails.ManagerNetworkName = truck.ManagerNetworkName;
                //truckDetails.Status = truck.Status;
                //truckDetails.IgnitionStatus = truck.IgnitionStatus;
                //truckDetails.IsTrailerSkirt = truck.IsTrailerSkirt;
                //truckDetails.IsInShop = truck.IsInShop;
                //truckDetails.TruckInShopDescription = truck.TruckInShopDescription;

                await _trucks.ReplaceOneAsync(m => m.Id == truckDetails.Id, truck);
            }
            return truckDetails;
        }

        public async Task<List<Truck>> GetSearchedTruckAsync(string searchTerm)
        {
            var search = Builders<Truck>.Filter.Regex(x => x.Number, new BsonRegularExpression(searchTerm, "i"));
            var searchedTrucks = await _trucks.Find(search).ToListAsync();
            return searchedTrucks;
        }
    }
}
