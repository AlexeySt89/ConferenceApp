
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<Participant>> GetAllPar();
        Task<bool> Remove(string email);
    }
}
