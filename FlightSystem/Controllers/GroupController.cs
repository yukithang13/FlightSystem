using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public GroupController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        [Authorize(Roles = "User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGroupByAsync()
        {
            try
            {
                return Ok(await _uow.GroupService.GetAllGroupByAsync());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Authorize(Roles = "User")]

        [HttpGet("get-id/{id}")]
        public async Task<IActionResult> GetGroupByIdAsync(int id)
        {
            var gr = await _uow.GroupService.GetGroupByIdAsync(id);
            return gr == null ? NotFound() : Ok(gr);
        }


        [Authorize(Roles = "Admin,Pilot")]
        [HttpPost]
        public async Task<IActionResult> AddGroupAsync([FromBody] GroupModel groupmodel)
        {
            var newG = await _uow.GroupService.AddGroupAsync(groupmodel);
            return Ok(newG);
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupAsync(int id, [FromBody] GroupModel groupmodel)
        {
            if (id != groupmodel.GroupId)
            {
                return NotFound();
            }
            await _uow.GroupService.UpdateGroupAsync(id, groupmodel);
            return Ok();
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupAsync([FromBody] int id)
        {
            await _uow.GroupService.DeleteGroupAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpGet("search-date-range")]
        public async Task<ActionResult> SearchGroupsByDateRange([FromQuery] string startDate, [FromQuery] string endDate)
        {
            try
            {
                DateTime startDateParsed, endDateParsed;

                if (DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateParsed) &&
                    DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateParsed))
                {
                    var group = await _uow.GroupService.SearchGroupsByDateRangeAsync(startDateParsed, endDateParsed);

                    return Ok(group);
                }
                else
                {
                    return BadRequest("Format error. You can use dd-MM-yyyy.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize(Roles = "Admin,Pilot,Crew,User")]

        [HttpGet("search-term")]
        public async Task<ActionResult> SearchGroups([FromQuery] string searchTerm)
        {
            try
            {
                var documents = await _uow.GroupService.SearchGroupsAsync(searchTerm);
                return Ok(documents);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
