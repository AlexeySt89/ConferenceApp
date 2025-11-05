using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParProgramCommRepository : IParProgramCommRepository
    {
        private readonly AppDbContext _context;
        public ParProgramCommRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(ParProgramComm member)
        {
            await _context.ParProgramComms.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var member = await _context.ParProgramComms.FirstOrDefaultAsync(x => x.Id == id);

            if (member == null)
                return false;

            _context.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<ParProgramComm>> GetAllAsync()
        {
            return await _context.ParProgramComms.ToListAsync();
        }

        public Task<ParProgramComm?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ParProgramComm member)
        {
            _context.ParProgramComms.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
