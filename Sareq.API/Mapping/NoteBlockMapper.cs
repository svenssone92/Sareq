using Sareq.API.Models.NoteBlocks;
using Sareq.API.Models;
using Sareq.Shared.DTOs;
using Sareq.API.Converters;

namespace Sareq.API.Mapping
{
    public static class NoteBlockMapper
    {
        public static NoteBlockDto ToDto(NoteBlock element)
        {
            return element switch
            {
                TextBlock text => new TextBlockDto
                {
                    Id = text.Id,
                    Order = text.Order,
                    EditorStateJson = QuillEditorJsonConverter.FromSpans(text.Spans)
                },
                _ => throw new NotImplementedException("Unknown block type")
            };
        }

        public static NoteBlock ToDomain(NoteBlockDto dto)
        {
            return dto switch
            {
                TextBlockDto text => new TextBlock
                {
                    Order = text.Order,
                    Spans = QuillEditorJsonConverter.ToSpans(text.EditorStateJson)
                },
                _ => throw new NotImplementedException("Unknown DTO block type")
            };
        }
    }
}
