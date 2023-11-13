using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupInfoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public GroupInfoController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [Authorize(Roles = "User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGroupInfoByAsync()
        {
            try
            {
                return Ok(await _uow.GroupInfoService.GetAllGroupInfoByAsync());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Authorize(Roles = "User")]

        [HttpGet("get-id/{id}")]
        public async Task<IActionResult> GetGroupInfoByIdAsync(int id)
        {
            var gr = await _uow.GroupInfoService.GetGroupInfoByIdAsync(id);
            return gr == null ? NotFound() : Ok(gr);
        }


        [Authorize(Roles = "Admin,Pilot")]
        [HttpPost]
        public async Task<IActionResult> AddGroupInfoAsync([FromBody] GroupInfoModel groupinfomodel)
        {
            var newG = await _uow.GroupInfoService.AddGroupInfoAsync(groupinfomodel);
            return Ok(newG);
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupInfoAsync(int id, [FromBody] GroupInfoModel groupinfomodel)
        {
            if (id != groupinfomodel.GroupInfoId)
            {
                return NotFound();
            }
            await _uow.GroupInfoService.UpdateGroupInfoAsync(id, groupinfomodel);
            return Ok();
        }

        [Authorize(Roles = "Admin,Pilot")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupInfoAsync([FromBody] int id)
        {
            await _uow.GroupInfoService.DeleteGroupInfoAsync(id);
            return Ok();
        }
    }
}
