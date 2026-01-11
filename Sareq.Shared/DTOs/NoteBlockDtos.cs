namespace Sareq.Shared.DTOs
{
    public abstract class NoteBlockDto
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public abstract string Type { get; }
    }

    public class TextBlockDto : NoteBlockDto
    {
        public override string Type => "text";
        public string EditorStateJson { get; set; } = string.Empty;
    }
}
