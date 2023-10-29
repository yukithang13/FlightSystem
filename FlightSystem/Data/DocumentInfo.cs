using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class DocumentInfo
    {
        [Key]
        public int DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int FlightId { get; set; }
        public double Version { get; set; } = 1;
        public int Type { get; set; }
        public int GroupId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CreaterID { get; set; }
    }
}
