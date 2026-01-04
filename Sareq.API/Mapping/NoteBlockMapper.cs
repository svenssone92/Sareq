using Sareq.API.Models.NoteBlocks;
using Sareq.API.Models;
using Sareq.Shared.DTOs;

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
                    //Not yet implemented: JsonString = EditorJsonConverter.FromSpans(element.Spans)
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
                    Id = text.Id,
                    Order = text.Order,
                    //Not yet implemented: Spans = EditorJsonConverter.ToSpans(dto.EditorStateJson)
                },
                _ => throw new NotImplementedException("Unknown DTO block type")
            };
        }
    }
}
