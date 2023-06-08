using FleetForceAPI.Models;
using MongoDB.Driver;

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

        public async Task<Truck> UpdateTruckAsync(Truck truck)
        {
            var truckDetails = Details(truck.Id);
            if (truckDetails is not null)
            {
                // Automapper or any mapping libraries or function is usefull to map these properties together.
                truckDetails.Number = truck.Number;
                truckDetails.Company = truck.Company;
                truckDetails.AssetOwner = truck.AssetOwner;
                truckDetails.As400CoverageName = truck.As400CoverageName;
                truckDetails.CoveringManager = truck.CoveringManager;
                truckDetails.ManagerNetworkName = truck.ManagerNetworkName;
                truckDetails.Status = truck.Status;
                truckDetails.IgnitionStatus = truck.IgnitionStatus;
                truckDetails.IsTrailerSkirt = truck.IsTrailerSkirt;
                truckDetails.IsInShop = truck.IsInShop;
                truckDetails.TruckInShopDescription = truck.TruckInShopDescription;

                await _trucks.ReplaceOneAsync(m => m.Id == truckDetails.Id, truck);
            }
            return truckDetails;
        }
    }
}
