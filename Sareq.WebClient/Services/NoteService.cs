using Microsoft.Extensions.Logging;
using Sareq.Shared.DTOs;
using System.Net.Http.Json;

namespace Sareq.WebClient.Services
{
    public class NoteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NoteService> _logger;

        public NoteService(HttpClient httpClient, ILogger<NoteService> logger) 
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ListedNoteDto>> GetNoteListAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<ListedNoteDto>>("api/notes");
                return response ?? Enumerable.Empty<ListedNoteDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching note list: {ex.Message}");
                return Enumerable.Empty<ListedNoteDto>();
            }
        }
    }
}
