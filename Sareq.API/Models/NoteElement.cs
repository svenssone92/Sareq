namespace Sareq.API.Models
{
    public abstract class NoteElement
    {
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; } = null!;
    }
}
