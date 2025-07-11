using Sareq.API.Models;

namespace Sareq.API.Repositorys.Contracts
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync();
        Task<Note?> GetByIdAsync(int id);
        Task<Note> CreateAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(Note note);
    }
}
