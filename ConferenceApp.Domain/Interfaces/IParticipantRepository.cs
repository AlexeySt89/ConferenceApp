using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParticipantRepository
    {
        List<Participant> GetAll();
        Task<Participant?> GetByCredentialsAsync(string email);
        Task SaveAsync(Participant participant);
        Task UpdateAsync(string email, Participant participant);
    }
}
