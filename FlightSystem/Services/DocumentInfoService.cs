using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;

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


    }
}
