namespace Sareq.API.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<NoteElement> Elements { get; set; } = new();

        public DateTime DateMade { get; private set; } = DateTime.UtcNow;

        public bool IsPinned { get; set; }

    }
}
