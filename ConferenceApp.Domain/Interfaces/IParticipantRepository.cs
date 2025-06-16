using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParticipantRepository
    {
        Task<List<Participant>> GetAll();
        Task<List<Participant?>> GetApprovedPar();
        Task<Participant?> GetByCredentialsAsync(Guid email);
        Task<Participant?> GetByCredentialsAsync(string email);
        Task SaveAsync(Participant participant);
        Task UpdateAsync(Participant participant);
        Task UpdateAsync(string email, Participant participant);
        Task GetFileByIdAsync(Guid id);
    }
}
