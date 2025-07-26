using Sareq.Shared.DTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sareq.Shared.Converters
{
    public class NoteElementDtoConverter : JsonConverter<NoteElementDto>
    {
        public override NoteElementDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                string type = root.GetProperty("Type").GetString() ?? throw new JsonException("Type property is missing.");
                return type switch
                {
                    nameof(TextElementDto) => JsonSerializer.Deserialize<TextElementDto>(root.GetRawText(), options) ?? throw new JsonException("Failed to deserialize TextElementDto."),
                    _ => throw new JsonException($"Unknown NoteElementDto type: {type}")
                };
            }
        }
        public override void Write(Utf8JsonWriter writer, NoteElementDto value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
