using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Model
{
    public class FlightModel
    {
        [Required]
        public int FlightId { get; set; }

        [Required]
        public string FlightName { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedFlight { get; set; } = DateTime.Now;
        [Required]
        public string User { get; set; }
        [Required]
        public string StartPoint { get; set; } = string.Empty;
        [Required]
        public string EndPoint { get; set; } = string.Empty;
    }
}
