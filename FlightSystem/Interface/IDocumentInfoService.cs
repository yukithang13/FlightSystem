using FlightSystem.Data;
using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IDocumentInfoService
    {
        Task DeleteDocumentAsync(int id);
        Task<List<DocumentInfo>> GetAllDocumentByAsync();
        List<DocumentInfo> GetAllDocumentInfosQuickSort();
        Task<DocumentInfo> GetDocumentByIdAsync(int id);
        Task PostDocumentAsync(DocumentModel model, string userId, int flightId);
        Task<List<DocumentInfo>> SearchDocumentsAsync(string searchTerm);
        Task<List<DocumentInfo>> SearchDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task UpdateDocumentAsync(int id, DocumentModel documentmodel);
    }
}
