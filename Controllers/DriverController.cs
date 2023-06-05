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
        public IActionResult Gets()
        {
            var driver = _driverRepository.List();
            if (driver.Any())
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<DriverDTO>>(driver));
            }
            return StatusCode(StatusCodes.Status204NoContent);
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
            var driver = _driverRepository.DeleteDriver(id);
            if (driver is true)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DriverDTO driverDTO)
        {
            var mapModel = _mapper.Map<Driver>(driverDTO);
            var result = await _driverRepository.AddDriver(mapModel);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<DriverDTO>(result));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DriverDTO driverDTO)
        {
            var mapModel = _mapper.Map<Driver>(driverDTO);
            var result = await _driverRepository.UpdateDriver(mapModel);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<DriverDTO>(result));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
