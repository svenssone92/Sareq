namespace Sareq.Shared.DTOs
{
    public abstract class NoteElementDto
    {
        public int Id { get; set; }

        public string Type => GetType().Name;
    }

    public class TextElementDto : NoteElementDto
    {
        public string Text { get; set; } = string.Empty;
    }
}
