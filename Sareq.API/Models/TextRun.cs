namespace Sareq.API.Models
{
    public class TextRun
    {
        public string Text { get; set; } = string.Empty;
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public string? Color { get; set; }
        public int? FontSize { get; set; }
    }
}
