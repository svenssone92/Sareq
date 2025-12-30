namespace Sareq.API.Models.NoteElements
{
    public class TextElement : NoteElement

    {
        public List<TextSpan> Spans { get; set; } = new();
    }
}
