namespace FlightSystem.Model
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int FlightIdDocx { get; set; }
        public double Version { get; set; } = 1;
        public int Type { get; set; }
        public int GroupId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid CreaterID { get; set; }
    }
}
