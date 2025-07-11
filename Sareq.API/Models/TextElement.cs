using Sareq.API.Models.Interfaces;

namespace Sareq.API.Models
{
    public class TextElement : INoteElement

    {
        public int Id { get; set; }
        public int NoteId { get; set; }

        public List<TextRun> Runs { get; set; } = new();
    }
}
