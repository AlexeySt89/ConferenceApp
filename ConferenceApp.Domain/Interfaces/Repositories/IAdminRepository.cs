using ConferenceApp.Domain.Common.ValueObjects;

namespace ConferenceApp.Domain.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task AddAsync(Admin admin);
        Task<Admin?> GetByEmailAsync(Email email);
    }
}
