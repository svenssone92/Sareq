using Sareq.API.Models.RichText;

namespace Sareq.API.Models.NoteBlocks
{
    public class TextBlock : NoteBlock

    {
        public List<TextSpan> Spans { get; set; } = new();
    }
}
