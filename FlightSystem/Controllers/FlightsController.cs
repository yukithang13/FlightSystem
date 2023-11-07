using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public FlightsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetAllFlightByAsync()
        {
            try
            {
                return Ok(await _uow.FlightService.GetAllFlightByAsync());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightByIdAsync(int id)
        {
            var gv = await _uow.FlightService.GetFlightByIdAsync(id);
            return gv == null ? NotFound() : Ok(gv);
        }

        [HttpGet("page-flight")]
        public async Task<IActionResult> GetFlightByPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) // default 1-10 size
        {
            try
            {
                var pagedList = await _uow.FlightService.GetFlightByPageAsync(pageNumber, pageSize);
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("search-flight")]
        public async Task<IActionResult> FindFlightByPageAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, string searchString = "") // default 1-10 size
        {
            try
            {
                var pagedList = await _uow.FlightService.FindFlightByPageAsync(pageNumber, pageSize, searchString);
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddFlightAsync([FromBody] FlightModel flightmodel)
        {
            var userId = User.FindFirst("userId")?.Value;

            var newFl = await _uow.FlightService.AddFlightAsync(flightmodel, userId);
            return Ok(newFl);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlightAsync(int id, [FromBody] FlightModel flightmodel)
        {
            if (id != flightmodel.FlightId)
            {
                return NotFound();
            }
            await _uow.FlightService.UpdateFlightAsync(id, flightmodel);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlightAsync([FromBody] int id)
        {
            await _uow.FlightService.DeleteFlightAsync(id);
            return Ok();
        }
    }
}
