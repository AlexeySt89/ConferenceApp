using Microsoft.EntityFrameworkCore;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly AppDbContext _context;

        public ConferenceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Conference conference)
        {
            await _context.Conferences.AddAsync(conference);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Conference>> GetAllAsync()
        {
            return await _context.Conferences.Include(c => c.Participants).ToListAsync();
        }

        public async Task<Conference?> GetByIdAsync(Guid id)
        {
            return await _context.Conferences.Include(c => c.Participants)
                                              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateAsync(Conference conference)
        {
            throw new NotImplementedException();
        }
    }
}
