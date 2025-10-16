using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParticipantRepository
    {
        Task<Participant?> GetByIdAsync(Guid id);
        Task<Participant?> GetByEmailAsync(Email email);

        Task<IReadOnlyList<Participant>> GetAllAsync();
        Task<IReadOnlyList<Participant>> GetApprovedAsync();
        Task<IReadOnlyList<Participant>> GetPendingAsync();

        Task AddAsync(Participant participant);
        Task UpdateAsync(Participant participant);
        Task<bool> UpdateApprovalStatusAsync(Email email, bool isApproved, string? rejectReason = null);

        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Email email);
    }
}
