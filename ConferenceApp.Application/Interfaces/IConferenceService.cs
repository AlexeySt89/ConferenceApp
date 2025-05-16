using ConferenceApp.Application.DTOs;

namespace ConferenceApp.Application.Interfaces
{
    public interface IConferenceService
    {
        Task<List<ConferenceDto>> GetConferencesAsync();
        Task<ConferenceDto> GetConferenceByIdAsync(Guid id);
    }
}
