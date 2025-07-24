namespace Sareq.API.Models
{
    public class NoteView
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; } = null!;
        public DateTime ViewedAt { get; set; }
    }

}
