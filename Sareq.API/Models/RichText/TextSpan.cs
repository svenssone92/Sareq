namespace Sareq.API.Models.RichText
{
    public class TextSpan
    {
        public string Text { get; set; } = string.Empty;

        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strike { get; set; }

        public string? Color { get; set; }
        public string? Background { get; set; }

        public string? Link { get; set; }
    }
}
