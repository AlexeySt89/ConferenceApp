using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParProgramCommRepository
    {
        Task<IReadOnlyList<ParProgramComm>> GetAllAsync();
        Task<ParProgramComm?> GetByIdAsync(Guid id);
        Task AddAsync(ParProgramComm member);
        Task UpdateAsync(ParProgramComm member);
        Task<bool> DeleteAsync(Guid id);
    }
}
