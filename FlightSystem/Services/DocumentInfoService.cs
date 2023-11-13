using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Services
{
    public class DocumentInfoService : IDocumentInfoService
    {

        private readonly IWebHostEnvironment hostEnvironment;
        private FlightSystemDBContext _dbcontext;
        private IMapper _mapper;

        public DocumentInfoService(FlightSystemDBContext dbcontext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            this._dbcontext = dbcontext;
            this._mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }



        // find time
        public async Task<List<DocumentInfo>> SearchDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbcontext.DocumentInfos
                .Where(d => d.CreatedAt >= startDate && d.CreatedAt <= endDate)
                .ToListAsync();
        }
        // Find name, file, version
        public async Task<List<DocumentInfo>> SearchDocumentsAsync(string searchTerm)
        {
            return await _dbcontext.DocumentInfos
                .Where(d => d.Title.Contains(searchTerm) || d.FileData.Contains(searchTerm) || d.Version.ToString().Contains(searchTerm))
                .ToListAsync();
        }
        //get all
        public async Task<List<DocumentInfo>> GetAllDocumentByAsync()
        {
            var docx = await _dbcontext.DocumentInfos.ToListAsync();
            return _mapper.Map<List<DocumentInfo>>(docx);
        }

        // get 1 id
        public async Task<DocumentInfo> GetDocumentByIdAsync(int id)
        {
            var fl = await _dbcontext.DocumentInfos.FindAsync(id);
            return _mapper.Map<DocumentInfo>(fl);
        }

        public async Task PostDocumentAsync(DocumentModel model, string userId, int flightId)
        {
            try
            {
                var file = model.FileData;
                if (file != null && file.Length > 0)
                {
                    var uploadsFolderPath = Path.Combine(hostEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(uploadsFolderPath))
                        Directory.CreateDirectory(uploadsFolderPath);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var flight = _dbcontext.Flights.FirstOrDefault(f => f.FlightName == model.FlightName);
                    if (flight == null)
                    {
                        throw new Exception("Flight error");
                    }

                    var document = _mapper.Map<DocumentInfo>(model);
                    document.FileData = filePath;
                    document.CreaterID = userId;
                    document.FlightIdDocx = flight.FlightId;

                    var result = _dbcontext.DocumentInfos.Add(document);

                    await _dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //update
        public async Task UpdateDocumentAsync(int id, DocumentModel documentmodel)
        {
            var existingDocx = await _dbcontext.DocumentInfos.FindAsync(documentmodel);

            if (existingDocx == null)
            {
                throw new Exception("Docx not found");
            }
            if (id == documentmodel.DocumentInfoId)
            {
                var updateDocx = _mapper.Map<DocumentInfo>(documentmodel);
                _dbcontext.DocumentInfos.Update(updateDocx);
                await _dbcontext.SaveChangesAsync();
            }
        }

        // delete
        public async Task DeleteDocumentAsync(int id)
        {
            var deleteDocx = _dbcontext.DocumentInfos.SingleOrDefault(g => g.DocumentId == id);
            if (deleteDocx != null)
            {
                _dbcontext.DocumentInfos.Remove(deleteDocx);
                await _dbcontext.SaveChangesAsync();
            }
        }

        //Quick Sort
        public List<DocumentInfo> GetAllDocumentInfosQuickSort()
        {
            var documentInfos = _dbcontext.DocumentInfos.ToList();
            QuickSort(documentInfos, 0, documentInfos.Count - 1);
            return documentInfos;
        }

        private void QuickSort(List<DocumentInfo> documentInfos, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(documentInfos, low, high);

                QuickSort(documentInfos, low, pivot - 1);
                QuickSort(documentInfos, pivot + 1, high);
            }
        }

        private int Partition(List<DocumentInfo> documentInfos, int low, int high)
        {
            DocumentInfo pivot = documentInfos[high];

            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (documentInfos[j].DocumentId < pivot.DocumentId)
                {
                    i++;
                    DocumentInfo swapTemp = documentInfos[i];
                    documentInfos[i] = documentInfos[j];
                    documentInfos[j] = swapTemp;
                }
            }

            DocumentInfo pivotTemp = documentInfos[i + 1];
            documentInfos[i + 1] = documentInfos[high];
            documentInfos[high] = pivotTemp;

            return i + 1;
        }

    }
}
