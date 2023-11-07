using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentInfoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public DocumentInfoController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        /// <summary>
        /// Multiple File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>


        [HttpPost("PostMultipleFile")]
        public async Task<ActionResult> PostMultipleFile([FromForm] DocumentModel model)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (model == null)
            {
                return BadRequest();
            }

            try
            {

                int flightId = _uow.FlightService.GetFlightIdFromFlightName(model.FlightName);

                await _uow.DocumentInfoService.PostDocumentAsync(model, userId, flightId);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
