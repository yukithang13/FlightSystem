using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class GroupInfo
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
