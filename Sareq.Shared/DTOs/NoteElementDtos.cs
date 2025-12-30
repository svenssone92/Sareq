namespace Sareq.Shared.DTOs
{
    public abstract class NoteElementDto
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public abstract string Type { get; }
    }

    public class TextElementDto : NoteElementDto
    {
        public override string Type => "text";
        public string JsonString { get; set; } = string.Empty;
    }
}
