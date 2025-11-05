using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParOrgCommRepository : IParOrgCommRepository
    {
        private readonly AppDbContext _context;
        public ParOrgCommRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(ParOrgComm member)
        {
            await _context.ParOrgComms.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var member = await _context.ParOrgComms.FirstOrDefaultAsync(x => x.Id == id);

            if(member == null)
                return false;
            
            _context.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<ParOrgComm>> GetAllAsync()
        {
            return await _context.ParOrgComms.ToListAsync();
        }

        public Task<ParOrgComm?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ParOrgComm member)
        {
            _context.ParOrgComms.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
