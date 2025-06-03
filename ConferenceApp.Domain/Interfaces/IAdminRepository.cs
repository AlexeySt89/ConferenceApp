using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<Participant>> GetAllPar();
        Task<bool> RemovePar(string email);
    }
}
