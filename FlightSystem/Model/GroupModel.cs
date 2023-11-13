namespace FlightSystem.Model
{
    public class GroupModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
