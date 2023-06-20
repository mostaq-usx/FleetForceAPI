using AutoMapper;
using FleetForceAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;

namespace FleetForceAPI.Controllers
{
    public class CombineSearchController : ControllerBase
    {
        private readonly ITruckRepository _truckRepository;
        private readonly ILoadRepository _loadRepository;
        private readonly IDriverRepository _driverRepository;

        public CombineSearchController(ITruckRepository truckRepository, ILoadRepository loadRepository, IDriverRepository driverRepository)
        {
            _truckRepository = truckRepository;
            _loadRepository = loadRepository;
            _driverRepository = driverRepository;
        }

        [HttpGet("allSearch")]
        public async Task<IActionResult> FilterTrucks(string searchTerm)
        {
            var searchedTrucks = await _truckRepository.GetSearchedTruckAsync(searchTerm);
            var searchedLoads = await _loadRepository.GetSearchedLoadAsync(searchTerm);
            var searchedDrivers = await _driverRepository.GetSearchedDriverAsync(searchTerm);

            var combinedResults = searchedTrucks.Cast<object>().Concat(searchedLoads.Cast<object>()).Concat(searchedDrivers.Cast<Object>());

            return Ok(combinedResults);
        }
    }
}
