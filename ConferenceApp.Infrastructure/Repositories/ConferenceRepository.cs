using Microsoft.EntityFrameworkCore;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Infrastructure.Data;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly AppDbContext _context;

        public ConferenceRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Conference conference)
        {
            await _context.Conferences.AddAsync(conference);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Conferences.AnyAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyList<Conference>> GetAllAsync()
        {
            return await _context.Conferences.Include(c => c.Participants).ToListAsync();
        }

        public async Task<Conference?> GetByIdAsync(Guid id)
        {
            return await _context.Conferences.Include(c => c.Participants).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Conference conference)
        {
            _context.Conferences.Update(conference);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var conference = await GetByIdAsync(id);
            if (conference == null)
                return false;

            _context.Conferences.Remove(conference);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
