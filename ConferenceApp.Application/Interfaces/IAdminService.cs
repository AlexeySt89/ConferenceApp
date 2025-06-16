
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<Participant>> GetAllPar();
        Task<bool> UpdateParStatusAsync(string email, bool status);
        Task<bool> Remove(string email);
        Task<List<Participant>> GetApprovePar();
    }
}
