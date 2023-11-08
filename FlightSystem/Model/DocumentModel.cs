using FlightSystem.Helpers;

namespace FlightSystem.Model
{
    public class DocumentModel
    {
        public int DocumentInfoId { get; set; }
        public IFormFile FileData { get; set; }
        public FileType FileType { get; set; }

        public string FlightName { get; set; }
        public string Version { get; set; }

    }
}
