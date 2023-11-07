using FlightSystem.Helpers;

namespace FlightSystem.Model
{
    public class DocumentModel
    {
        public IFormFile DocumentInfo { get; set; }
        public FileType FileType { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
