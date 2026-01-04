namespace Sareq.API.Models
{
    public abstract class NoteBlock
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; } = null!;
    }
}
