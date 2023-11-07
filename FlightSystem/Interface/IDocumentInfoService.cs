using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IDocumentInfoService
    {
        Task PostMultiFileAsync(List<DocumentModel> fileData, string userId);
    }
}
