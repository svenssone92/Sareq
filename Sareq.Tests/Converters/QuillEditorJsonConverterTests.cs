using System.Text.Json;
using Sareq.API.Converters;
using Sareq.API.Models.RichText;

namespace Sareq.Tests.Converters
{
    public class QuillEditorJsonConverterTests
    {
        [Fact]
        public void ToSpans_ShouldParseQuillJson_WithBoldAttribute()
        {
            // Arrange
            var json = @"{
              ""ops"": [
                { ""insert"": ""Hello "" },
                { ""insert"": ""world"", ""attributes"": { ""bold"": true } },
                { ""insert"": ""\n"" }
              ]
            }";

            // Act
            var spans = QuillEditorJsonConverter.ToSpans(json);

            // Assert
            Assert.Equal(2, spans.Count);
            Assert.Equal("Hello ", spans[0].Text);
            Assert.False(spans[0].Bold);
            Assert.Equal("world", spans[1].Text);
            Assert.True(spans[1].Bold);
        }

        [Fact]
        public void FromSpans_ShouldProduceValidQuillJson_WithAttributes()
        {
            // Arrange
            var spans = new[]
            {
                new TextSpan { Text = "Hello " },
                new TextSpan { Text = "world", Bold = true }
            };

            // Act
            var resultJson = QuillEditorJsonConverter.FromSpans(spans);

            using var doc = JsonDocument.Parse(resultJson);
            var root = doc.RootElement;
            var ops = root.GetProperty("ops").EnumerateArray().ToArray();

            // Assert
            // two inserts + trailing newline => 3 ops
            Assert.Equal(3, ops.Length);

            Assert.Equal("Hello ", ops[0].GetProperty("insert").GetString());

            var secondOp = ops[1];
            Assert.Equal("world", secondOp.GetProperty("insert").GetString());
            Assert.True(secondOp.TryGetProperty("attributes", out var attrs));
            Assert.True(attrs.GetProperty("bold").GetBoolean());

            Assert.Equal("\n", ops[2].GetProperty("insert").GetString());
        }

        [Fact]
        public void RoundTrip_ToSpans_FromSpans_PreservesTextAndAttributes()
        {
            // Arrange
            var original = new[]
            {
                new TextSpan { Text = "A", Italic = true },
                new TextSpan { Text = "B", Underline = true, Color = "#ff0000" },
                new TextSpan { Text = "C", Link = "https://example.com" }
            };

            // Act
            var json = QuillEditorJsonConverter.FromSpans(original);
            var parsed = QuillEditorJsonConverter.ToSpans(json);

            // Assert - ToSpans ignores the trailing newline, so counts should match original
            Assert.Equal(original.Length, parsed.Count);

            for (int i = 0; i < original.Length; i++)
            {
                Assert.Equal(original[i].Text, parsed[i].Text);
                Assert.Equal(original[i].Bold, parsed[i].Bold);
                Assert.Equal(original[i].Italic, parsed[i].Italic);
                Assert.Equal(original[i].Underline, parsed[i].Underline);
                Assert.Equal(original[i].Strike, parsed[i].Strike);
                Assert.Equal(original[i].Color, parsed[i].Color);
                Assert.Equal(original[i].Background, parsed[i].Background);
                Assert.Equal(original[i].Link, parsed[i].Link);
            }
        }
    }
}
