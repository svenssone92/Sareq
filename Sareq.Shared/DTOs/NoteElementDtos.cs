namespace Sareq.Shared.DTOs
{
    public abstract class NoteElementDto
    {
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public string Type => GetType().Name;
    }

    public class TextElementDto : NoteElementDto
    {
        public string Text { get; set; } = string.Empty;
    }
}
