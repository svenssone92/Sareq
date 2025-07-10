namespace Sareq.API.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateTime DateMade { get; private set; } = DateTime.UtcNow;
        public DateTime? DateEdited { get; set; }

        public int ViewCount { get; private set; }
        public DateTime? LastViewed { get; private set; }

        public bool IsPinned { get; set; }

        public void MarkEdited()
        {
            DateEdited = DateTime.UtcNow;
        }

        public void IncrementView()
        {
            ViewCount++;
            LastViewed = DateTime.UtcNow;
        }

    }
}
