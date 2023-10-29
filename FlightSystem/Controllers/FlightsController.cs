using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightServ;
        public FlightsController(IFlightService serv)
        {
            _flightServ = serv;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlightByAsync()
        {
            try
            {
                return Ok(await _flightServ.GetAllFlightByAsync());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightByIdAsync(int id)
        {
            var gv = await _flightServ.GetFlightByIdAsync(id);
            return gv == null ? NotFound() : Ok(gv);
        }

        [HttpGet("page-flight")]
        public async Task<IActionResult> GetFlightByPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) // default 1-10 size
        {
            try
            {
                var pagedList = await _flightServ.GetFlightByPageAsync(pageNumber, pageSize);
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
                var pagedList = await _flightServ.FindFlightByPageAsync(pageNumber, pageSize, searchString);
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddFlightAsync(FlightModel flightmodel)
        {
            var newFl = await _flightServ.AddFlightAsync(flightmodel);
            var fl = await _flightServ.GetFlightByIdAsync(newFl);
            return fl == null ? NotFound() : Ok(fl);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlightAsync(int id, [FromBody] FlightModel flightmodel)
        {
            if (id != flightmodel.FlightId)
            {
                return NotFound();
            }
            await _flightServ.UpdateFlightAsync(id, flightmodel);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlightAsync([FromBody] int id)
        {
            await _flightServ.DeleteFlightAsync(id);
            return Ok();
        }
    }
}
