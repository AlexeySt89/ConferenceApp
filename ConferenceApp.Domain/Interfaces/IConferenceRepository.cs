using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IConferenceRepository
    {
        Task<Conference?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Conference>> GetAllAsync();
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(Conference conference);
        Task UpdateAsync(Conference conference);
        Task<bool> DeleteAsync(Guid id);
    }
}
