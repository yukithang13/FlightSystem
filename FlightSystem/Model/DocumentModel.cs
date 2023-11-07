using FlightSystem.Helpers;

namespace FlightSystem.Model
{
    public class DocumentModel
    {
        public IFormFile FileData { get; set; }
        public FileType FileType { get; set; }

        public string FlightName { get; set; }
        public string Version { get; set; }

    }
}
