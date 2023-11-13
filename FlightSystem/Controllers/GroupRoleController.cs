using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRoleController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public GroupRoleController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Authorize(Roles = "User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGroupRoleByAsync()
        {
            try
            {
                return Ok(await _uow.GroupRoleService.GetAllGroupRoleByAsync());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Authorize(Roles = "User")]

        [HttpGet("get-id/{id}")]
        public async Task<IActionResult> GetGroupRoleByIdAsync(int id)
        {
            var gr = await _uow.GroupRoleService.GetGroupRoleByIdAsync(id);
            return gr == null ? NotFound() : Ok(gr);
        }


        [Authorize(Roles = "Admin,Pilot")]
        [HttpPost]
        public async Task<IActionResult> AddGroupRoleAsync([FromBody] GroupRoleModel grouprolemodel)
        {
            var newG = await _uow.GroupRoleService.AddGroupRoleAsync(grouprolemodel);
            return Ok(newG);
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupRoleAsync(int id, [FromBody] GroupRoleModel grouprolemodel)
        {
            if (id != grouprolemodel.GroupRoleId)
            {
                return NotFound();
            }
            await _uow.GroupRoleService.UpdateGroupRoleAsync(id, grouprolemodel);
            return Ok();
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupRoleAsync([FromBody] int id)
        {
            await _uow.GroupRoleService.DeleteGroupRoleAsync(id);
            return Ok();
        }
    }
}
