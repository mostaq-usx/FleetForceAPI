using FleetForceAPI.Models;

namespace FleetForceAPI.Repository
{
    public interface ILoadRepository
    {
        Task<Load> AddLoadAsync(Load load);

        Task<Load> UpdateLoadAsync(Load load);

        bool DeleteLoad(string id);

        Task<IEnumerable<Load>> GetAllLoadsListAsync();

        Task<List<Load>> GetFilterLoadsAsync(string filterby);

        Task<List<Load>> GetSortLoadsAsync(string sortOrder);

        Task<List<Load>> GetPagedLoadsAsync(int page, int pageSize);

        Load Details(string id);

        Task<List<Load>> GetSearchedLoadAsync(string searchTerm);
    }
}
