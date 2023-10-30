using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
