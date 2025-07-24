using Sareq.API.Models;

namespace Sareq.API.Repository.Contracts
{
    public interface INoteViewRepository
    {
        Task AddAsync(NoteView view);
        Task<IEnumerable<Note>> GetMostViewedAsync(DateTime since, int count);
        Task DeletePastYearAsync();
    }
}
