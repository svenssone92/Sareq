using Sareq.API.Models.RichText;
using System.Text.Json;

namespace Sareq.API.Converters
{
    public static class QuillEditorJsonConverter
    {
        // -------------------------
        // Editor JSON → Domain
        // -------------------------
        public static List<TextSpan> ToSpans(string editorJson)
        {
            using var doc = JsonDocument.Parse(editorJson);
            var ops = doc.RootElement.GetProperty("ops");

            var spans = new List<TextSpan>();

            foreach (var op in ops.EnumerateArray())
            {
                if (!op.TryGetProperty("insert", out var insertProp))
                    continue;

                var text = insertProp.GetString();
                if (string.IsNullOrEmpty(text) || text == "\n")
                    continue;

                var span = new TextSpan
                {
                    Text = text
                };

                if (op.TryGetProperty("attributes", out var attrs))
                {
                    span.Bold = attrs.TryGetProperty("bold", out _);
                    span.Italic = attrs.TryGetProperty("italic", out _);
                    span.Underline = attrs.TryGetProperty("underline", out _);
                    span.Strike = attrs.TryGetProperty("strike", out _);

                    if (attrs.TryGetProperty("color", out var color))
                        span.Color = color.GetString();

                    if (attrs.TryGetProperty("background", out var bg))
                        span.Background = bg.GetString();

                    if (attrs.TryGetProperty("link", out var link))
                        span.Link = link.GetString();
                }

                spans.Add(span);
            }

            return spans;
        }

        // -------------------------
        // Domain → Editor JSON
        // -------------------------
        public static string FromSpans(IEnumerable<TextSpan> spans)
        {
            var ops = spans.Select(span =>
            {
                var attributes = new Dictionary<string, object>();

                if (span.Bold) attributes["bold"] = true;
                if (span.Italic) attributes["italic"] = true;
                if (span.Underline) attributes["underline"] = true;
                if (span.Strike) attributes["strike"] = true;
                if (!string.IsNullOrEmpty(span.Color)) attributes["color"] = span.Color;
                if (!string.IsNullOrEmpty(span.Background)) attributes["background"] = span.Background;
                if (!string.IsNullOrEmpty(span.Link)) attributes["link"] = span.Link;

                return attributes.Count == 0
                    ? new { insert = span.Text }
                    : new { insert = span.Text, attributes };
            }).ToList();

            ops.Add(new { insert = "\n" });

            return JsonSerializer.Serialize(new { ops });
        }
    }
}
