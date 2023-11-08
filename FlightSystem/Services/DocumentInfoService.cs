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
        private FlightSystemDBContext dbcontext;
        private IMapper mapper;

        public DocumentInfoService(FlightSystemDBContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }

        public DocumentInfoService(FlightSystemDBContext dbcontext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }
        // find time
        public async Task<List<DocumentInfo>> SearchDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await dbcontext.DocumentInfos
                .Where(d => d.CreatedAt >= startDate && d.CreatedAt <= endDate)
                .ToListAsync();
        }
        // Find name, file, version
        public async Task<List<DocumentInfo>> SearchDocumentsAsync(string searchTerm)
        {
            return await dbcontext.DocumentInfos
                .Where(d => d.Title.Contains(searchTerm) || d.FileData.Contains(searchTerm) || d.Version.ToString().Contains(searchTerm))
                .ToListAsync();
        }
        //get all
        public async Task<List<DocumentInfo>> GetAllDocumentByAsync()
        {
            var docx = await dbcontext.DocumentInfos.ToListAsync();
            return mapper.Map<List<DocumentInfo>>(docx);
        }

        // get 1 id
        public async Task<DocumentInfo> GetDocumentByIdAsync(int id)
        {
            var fl = await dbcontext.DocumentInfos.FindAsync(id);
            return mapper.Map<DocumentInfo>(fl);
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

                    var flight = dbcontext.Flights.FirstOrDefault(f => f.FlightName == model.FlightName);
                    if (flight == null)
                    {
                        throw new Exception("Flight error");
                    }

                    var document = mapper.Map<DocumentInfo>(model);
                    document.FileData = filePath;
                    document.CreaterID = userId;
                    document.FlightIdDocx = flight.FlightId;

                    var result = dbcontext.DocumentInfos.Add(document);

                    await dbcontext.SaveChangesAsync();
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
            var existingDocx = await dbcontext.DocumentInfos.FindAsync(documentmodel);

            if (existingDocx == null)
            {
                throw new Exception("Docx not found");
            }
            if (id == documentmodel.DocumentInfoId)
            {
                var updateDocx = mapper.Map<DocumentInfo>(documentmodel);
                dbcontext.DocumentInfos.Update(updateDocx);
                await dbcontext.SaveChangesAsync();
            }
        }

        // delete
        public async Task DeleteDocumentAsync(int id)
        {
            var deleteDocx = dbcontext.DocumentInfos.SingleOrDefault(g => g.DocumentId == id);
            if (deleteDocx != null)
            {
                dbcontext.DocumentInfos.Remove(deleteDocx);
                await dbcontext.SaveChangesAsync();
            }
        }

    }
}
