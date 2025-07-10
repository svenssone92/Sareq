namespace Sareq.API.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateMade { get; set; } = DateTime.UtcNow;
        public DateTime DateEdited { get; private set; } = DateTime.UtcNow;
    }
}
