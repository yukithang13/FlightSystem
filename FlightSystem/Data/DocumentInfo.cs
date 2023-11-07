using FlightSystem.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class DocumentInfo
    {
        [Key]
        public int DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;

        public int FlightIdDocx { get; set; }

        public double Version { get; set; } = 1;
        public int Type { get; set; }
        [StringLength(450)]
        public string CreaterID { get; set; }

        public string FileData { get; set; }
        public FileType FileType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
