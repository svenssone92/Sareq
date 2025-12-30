namespace Sareq.API.Models
{
    public class TextSpan
    {
        public string Text { get; set; } = string.Empty;
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public string? Color { get; set; }
        public int? FontSize { get; set; }
        public string? Link { get; set; }
    }
}
