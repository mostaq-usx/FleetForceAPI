using AutoMapper;
using FleetForceAPI.DTO;
using FleetForceAPI.Models;
using FleetForceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FleetForceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ITruckRepository _truckRepository;
        private readonly IMapper _mapper;

        public TruckController(ITruckRepository truckRepository, IMapper mapper)
        {
            _truckRepository = truckRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Truck>>>> GetAllTrucksListAsync()
        {
            try
            {
                var trucks = await _truckRepository.GetAllTrucksListAsync();
                if (trucks is not null)
                {
                    var response = new ApiResponse<List<Truck>>
                    {
                        //TraceId = "7ae88305118242f980a4c2d79affa2aa",
                        IsSuccessful = true,
                        Data = trucks.ToList(),
                        Message = "Trucks list fetched successfully.",
                        Timestamp = DateTime.UtcNow
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("filterby")]
        public async Task<IActionResult> FilterTrucks([FromQuery] string filterby)
        {
            var trucks = await _truckRepository.GetFilterTrucksAsync(filterby);
            return Ok(trucks);
        }

        [HttpGet("sortBy")]
        public async Task<IActionResult> SortTrucks([FromQuery] string sortOrder)
        {
            if (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc")
            {
                return BadRequest("Invalid sortOrder value. Use 'asc' or 'desc'.");
            }

            var trucks = await _truckRepository.GetSortTrucksAsync(sortOrder);
            return Ok(trucks);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTrucks([FromQuery] int page, [FromQuery] int pageSize)
        {
            var trucks = await _truckRepository.GetPagedTrucksAsync(page, pageSize);
            return Ok(trucks);
        }

        [HttpGet("details")]
        public IActionResult Get(string id)
        {
            var truck = _truckRepository.Details(id);
            if (truck is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<TruckDTO>(truck));
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var truck = _truckRepository.DeleteTruck(id);
                if (truck is true)
                {
                    var response = new ApiResponse<List<Truck>>
                    {
                        //TraceId = truck.Id,
                        IsSuccessful = true,
                        //Data = truck.ToString(),
                        Message = "Truck deleted successfully.",
                        Timestamp = DateTime.UtcNow
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TruckDTO truckDTO)
        {
            try
            {
                var mapModel = _mapper.Map<Truck>(truckDTO);
                var truck = await _truckRepository.AddTruckAsync(mapModel);
                if (truck is not null)
                {
                    var response = new ApiResponse<List<Truck>>
                    {
                        TraceId = truck.Id,
                        IsSuccessful = true,
                        //Data = truck.ToString(),
                        Message = "Truck created successfully.",
                        Timestamp = DateTime.UtcNow
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(TruckDTO truckDTO)
        {
            try
            {
                var mapModel = _mapper.Map<Truck>(truckDTO);
                var truck = await _truckRepository.UpdateTruckAsync(mapModel);
                if (truck is not null)
                {
                    var response = new ApiResponse<List<Truck>>
                    {
                        TraceId = truck.Id,
                        IsSuccessful = true,
                        //Data = truck.ToString(),
                        Message = "Truck updated successfully.",
                        Timestamp = DateTime.UtcNow
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
