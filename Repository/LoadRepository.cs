using FleetForceAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FleetForceAPI.Repository
{
    public class LoadRepository : ILoadRepository
    {
        private readonly IMongoCollection<Load> _loads;

        public LoadRepository(IMongoDatabase mongoDatabase)
        {
            _loads = mongoDatabase.GetCollection<Load>("load");
        }

        public async Task<Load> AddLoadAsync(Load load)
        {
            if (load is not null)
            {
                load.Id = Guid.NewGuid().ToString();
                await _loads.InsertOneAsync(load);
            }
            return load;
        }

        public bool DeleteLoad(string id)
        {
            var load = Details(id);
            if (load is not null)
            {
                _loads.DeleteOne(m => m.Id == id);
                return true;
            }
            return false;
        }

        public Load Details(string id)
        {
            return _loads.Find(m => m.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Load>> GetAllLoadsListAsync()
        {
            await Task.Delay(100);

            return await _loads.Find(_ => true).ToListAsync();
        }

        public async Task<List<Load>> GetFilterLoadsAsync(string filterBy)
        {
            var filter = Builders<Load>.Filter.Regex(x => x.Number, new BsonRegularExpression(filterBy, "i"));
            var filteredLoads = await _loads.Find(filter).ToListAsync();
            return filteredLoads;
        }

        public async Task<List<Load>> GetSortLoadsAsync(string sortOrder)
        {
            var sortDefinition = Builders<Load>.Sort.Ascending(p => p.Number);

            if (sortOrder.ToLower() == "desc")
            {
                sortDefinition = Builders<Load>.Sort.Descending(p => p.Number);
            }

            var sortedLoads = await _loads.Find(Builders<Load>.Filter.Empty)
                                                   .Sort(sortDefinition)
                                                   .ToListAsync();
            return sortedLoads;
        }

        public async Task<List<Load>> GetPagedLoadsAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var loads = await _loads.Find(Builders<Load>.Filter.Empty)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
            return loads;
        }

        public async Task<Load> UpdateLoadAsync(Load load)
        {
            var loadDetails = Details(load.Id);
            if (loadDetails is not null)
            {
                // Automapper or any mapping libraries or function is usefull to map these properties together.
                loadDetails.Number = load.Number;
                loadDetails.Customer = load.Customer;
                loadDetails.LoadType = load.LoadType;
                loadDetails.Stops = load.Stops;

                await _loads.ReplaceOneAsync(m => m.Id == loadDetails.Id, load);
            }
            return loadDetails;
        }

        public async Task<List<Load>> GetSearchedLoadAsync(string searchTerm)
        {
            var search = Builders<Load>.Filter.Regex(x => x.Number, new BsonRegularExpression(searchTerm, "i"));
            var searchedLoads = await _loads.Find(search).ToListAsync();
            return searchedLoads;
        }
    }
}
