using ConferenceApp.Domain.Common.ValueObjects;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByEmailAsync(Email email);
        Task<Admin?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Email email);
        Task AddAsync(Admin admin);
        Task UpdateAsync(Admin admin);
    }
}
