using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Interfaces.Repositories;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext appDbContext) => _context = appDbContext;

        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }
    }
}
