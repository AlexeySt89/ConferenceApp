using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Admin?> GetByEmailAsync(Email email)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Email.Value == email.Value);
        }
    }
}
