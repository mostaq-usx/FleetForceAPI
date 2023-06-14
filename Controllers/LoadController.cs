using AutoMapper;
using FleetForceAPI.DTO;
using FleetForceAPI.Models;
using FleetForceAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FleetForceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadController : ControllerBase
    {
        private readonly ILoadRepository _loadRepository;
        private readonly IMapper _mapper;

        public LoadController(ILoadRepository loadRepository, IMapper mapper)
        {
            _loadRepository = loadRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Load>>>> GetAllLoadsListAsync()
        {
            try
            {
                var loads = await _loadRepository.GetAllLoadsListAsync();
                if (loads is not null)
                {
                    var response = new ApiResponse<List<Load>>
                    {
                        //TraceId = "7ae88305118242f980a4c2d79affa2aa",
                        IsSuccessful = true,
                        Data = loads.ToList(),
                        Message = "Loads list fetched successfully.",
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
        public async Task<IActionResult> FilterLoads([FromQuery] string filterby)
        {
            var loads = await _loadRepository.GetFilterLoadsAsync(filterby);
            return Ok(loads);
        }

        [HttpGet("sortBy")]
        public async Task<IActionResult> SortLoads([FromQuery] string sortOrder)
        {
            if (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc")
            {
                return BadRequest("Invalid sortOrder value. Use 'asc' or 'desc'.");
            }

            var loads = await _loadRepository.GetSortLoadsAsync(sortOrder);
            return Ok(loads);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedLoads([FromQuery] int page, [FromQuery] int pageSize)
        {
            var loads = await _loadRepository.GetPagedLoadsAsync(page, pageSize);
            return Ok(loads);
        }

        [HttpGet("details")]
        public IActionResult Get(string id)
        {
            var load = _loadRepository.Details(id);
            if (load is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<LoadDTO>(load));
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var load = _loadRepository.DeleteLoad(id);
                if (load is true)
                {
                    var response = new ApiResponse<List<Load>>
                    {
                        //TraceId = load.Id,
                        IsSuccessful = true,
                        //Data = load.ToString(),
                        Message = "Load deleted successfully.",
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
        public async Task<IActionResult> Post(LoadDTO loadDTO)
        {
            try
            {
                var mapModel = _mapper.Map<Load>(loadDTO);
                var load = await _loadRepository.AddLoadAsync(mapModel);
                if (load is not null)
                {
                    var response = new ApiResponse<List<Load>>
                    {
                        TraceId = load.Id,
                        IsSuccessful = true,
                        //Data = load.ToString(),
                        Message = "Load created successfully.",
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
        public async Task<IActionResult> Put(LoadDTO loadDTO)
        {
            try
            {
                var mapModel = _mapper.Map<Load>(loadDTO);
                var load = await _loadRepository.UpdateLoadAsync(mapModel);
                if (load is not null)
                {
                    var response = new ApiResponse<List<Load>>
                    {
                        TraceId = load.Id,
                        IsSuccessful = true,
                        //Data = load.ToString(),
                        Message = "Load updated successfully.",
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
