using AutoMapper;
using FleetForceAPI.DTO;
using FleetForceAPI.Models;
using FleetForceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FleetForceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverController(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Driver>>>> GetAllDriversListAsync()
        {
            try
            {
                var drivers = await _driverRepository.GetAllDriversListAsync();
                if (drivers is not null)
                {
                    var response = new ApiResponse<List<Driver>>
                    {
                        TraceId = "7ae88305118242f980a4c2d79affa2aa",
                        IsSuccessful = true,
                        Data = drivers.ToList(),
                        Message = "Driver list fetched successfully.",
                        Timestamp = DateTime.UtcNow
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("filterby")]
        public async Task<IActionResult> FilterDrivers([FromQuery] string filterby)
        {
            var drivers = await _driverRepository.GetFilterDriversAsync(filterby);
            return Ok(drivers);
        }

        [HttpGet("sortBy")]
        public async Task<IActionResult> SortDrivers([FromQuery] string sortOrder)
        {
            if (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc")
            {
                return BadRequest("Invalid sortOrder value. Use 'asc' or 'desc'.");
            }

            var drivers = await _driverRepository.GetSortDriversAsync(sortOrder);
            return Ok(drivers);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedDrivers([FromQuery] int page, [FromQuery] int pageSize)
        {
            var drivers = await _driverRepository.GetPagedDriversAsync(page, pageSize);
            return Ok(drivers);
        }

        [HttpGet("details")]
        public IActionResult Get(string id)
        {
            var driver = _driverRepository.Details(id);
            if (driver is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<DriverDTO>(driver));
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var driver = _driverRepository.DeleteDriver(id);
                if (driver is true)
                {
                    var response = new ApiResponse<List<Driver>>
                    {
                        //TraceId = driver.Id,
                        IsSuccessful = true,
                        //Data = driver.ToString(),
                        Message = "Driver deleted successfully.",
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
        public async Task<IActionResult> Post(DriverDTO driverDTO)
        {
            try
            {
                var mapModel = _mapper.Map<Driver>(driverDTO);
                var driver = await _driverRepository.AddDriverAsync(mapModel);
                if (driver is not null)
                {
                    var response = new ApiResponse<List<Driver>>
                    {
                        TraceId = driver.Id,
                        IsSuccessful = true,
                        //Data = driver.ToString(),
                        Message = "Driver created successfully.",
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
        public async Task<IActionResult> Put(DriverDTO driverDTO)
        {
            try
            {
                var mapModel = _mapper.Map<Driver>(driverDTO);
                var driver = await _driverRepository.UpdateDriverAsync(mapModel);
                if(driver is not null)
                {
                    var response = new ApiResponse<List<Driver>>
                    {
                        TraceId = driver.Id,
                        IsSuccessful = true,
                        //Data = driver.ToString(),
                        Message = "Driver updated successfully.",
                        Timestamp = DateTime.UtcNow
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
