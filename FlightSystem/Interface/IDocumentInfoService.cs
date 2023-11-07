using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IDocumentInfoService
    {
        Task PostDocumentAsync(DocumentModel model, string userId, int flightId);
    }
}
