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

        public async Task<NoteDto?> GetNoteAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<NoteDto>($"api/notes/{id}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching note with ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<NoteDto?> CreateNoteAsync(CreateNoteDto note)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/notes", note);

                if (!response.IsSuccessStatusCode)
                    return null;

                var createdNote = await response.Content.ReadFromJsonAsync<NoteDto>();
                return createdNote;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating note: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateNoteAsync(int id, UpdateNoteDto note)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/notes/{id}", note);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating note with ID {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/notes/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting note with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
