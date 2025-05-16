using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IConferenceRepository
    {
        Task<Conference?> GetByIdAsync(Guid id);
        Task<List<Conference>> GetAllAsync();
        Task AddAsync(Conference conference);
        Task UpdateAsync(Conference conference);
        Task DeleteAsync(Guid id);
    }
}
