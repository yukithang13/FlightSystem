using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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



        [HttpPost("add-document")]
        public async Task<ActionResult> AddDocumentAsync([FromForm] DocumentModel model)
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

        [HttpPut("update-document/{id}")]
        public async Task<IActionResult> UpdateDocumentAsync(int id, [FromBody] DocumentModel documentmodel)
        {
            if (id != documentmodel.DocumentInfoId)
            {
                return NotFound();
            }
            await _uow.DocumentInfoService.UpdateDocumentAsync(id, documentmodel);
            return Ok();
        }

        [HttpDelete("delete-document/{id}")]
        public async Task<IActionResult> DeleteDocumentAsync([FromBody] int id)
        {
            await _uow.DocumentInfoService.DeleteDocumentAsync(id);
            return Ok();
        }

        [HttpGet("docx-search-date-range")]
        public async Task<ActionResult> SearchDocumentsByDateRange([FromQuery] string startDate, [FromQuery] string endDate)
        {
            try
            {
                DateTime startDateParsed, endDateParsed;

                if (DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateParsed) &&
                    DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateParsed))
                {
                    var documents = await _uow.DocumentInfoService.SearchDocumentsByDateRangeAsync(startDateParsed, endDateParsed);

                    return Ok(documents);
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

        [HttpGet("docx-search-term")]
        public async Task<ActionResult> SearchDocuments([FromQuery] string searchTerm)
        {
            try
            {
                var documents = await _uow.DocumentInfoService.SearchDocumentsAsync(searchTerm);
                return Ok(documents);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
