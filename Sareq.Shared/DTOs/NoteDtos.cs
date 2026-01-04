namespace Sareq.Shared.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsPinned { get; set; }
        public DateTime DateMade { get; set; }
        public List<NoteBlockDto> Blocks { get; set; } = new();
    }

    public class ListedNoteDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsPinned { get; set; }
        public DateTime DateMade { get; set; }
    }

    public class CreateNoteDto
    {
        public string? Title { get; set; }
        public bool IsPinned { get; set; } = false;
        public List<NoteBlockDto> Blocks { get; set; } = new();
    }

    public class UpdateNoteDto
    {
        public string? Title { get; set; }
        public bool IsPinned { get; set; }
        public List<NoteBlockDto> Blocks { get; set; } = new();
    }
}

