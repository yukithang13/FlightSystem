using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        public string FlightName { get; set; } = string.Empty;
        public DateTime CreatedFlight { get; set; }
        public int DocumentId { get; set; }
        public string StartPoint { get; set; } = string.Empty;
        public string EndPoint { get; set; } = string.Empty;
    }
}
