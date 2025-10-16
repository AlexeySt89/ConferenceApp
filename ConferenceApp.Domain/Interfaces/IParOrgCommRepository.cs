using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParOrgCommRepository
    {
        Task<IReadOnlyList<ParOrgComm>> GetAllAsync();
        Task<ParOrgComm?> GetByIdAsync(Guid id);
        Task AddAsync(ParOrgComm member);
        Task UpdateAsync(ParOrgComm member);
        Task<bool> DeleteAsync(Guid id);
    }
}
