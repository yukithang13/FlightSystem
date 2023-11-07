using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentInfoController : ControllerBase
    {
        private readonly IDocumentInfoService _docxInfoServ;
        public DocumentInfoController(IDocumentInfoService serv)
        {
            _docxInfoServ = serv;
        }
        /// <summary>
        /// Multiple File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>


        [HttpPost("PostMultipleFile")]
        public async Task<ActionResult> PostMultipleFile([FromForm] List<DocumentModel> fileDetails)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _docxInfoServ.PostMultiFileAsync(fileDetails, userId);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
