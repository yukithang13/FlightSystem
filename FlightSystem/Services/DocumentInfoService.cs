using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;

namespace FlightSystem.Services
{
    public class DocumentInfoService : IDocumentInfoService
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;


        public DocumentInfoService(FlightSystemDBContext dbcontext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task PostMultiFileAsync(List<DocumentModel> fileData, string userId)
        {
            try
            {
                var uploadsFolderPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolderPath))
                    Directory.CreateDirectory(uploadsFolderPath);

                foreach (DocumentModel file in fileData)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.DocumentInfo.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.DocumentInfo.CopyTo(stream);
                    }

                    var fileDetails = new DocumentInfo()
                    {
                        Title = file.DocumentInfo.FileName,
                        FileType = file.FileType,
                        CreaterID = userId,
                        FileData = System.IO.File.ReadAllBytes(filePath), // Đọc dữ liệu từ tệp đã lưu
                        CreatedAt = DateTime.Now
                    };

                    var result = _dbcontext.DocumentInfos.Add(fileDetails);
                }
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
