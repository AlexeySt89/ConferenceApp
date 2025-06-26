using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _appDbContext;
        public AdminRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;
        public async Task<List<Participant>> GetAllPar()
        {
            if (_appDbContext.Participants is not null)
                return await _appDbContext.Participants.ToListAsync();
            else
                return new List<Participant>();
        }
        public async Task<List<Participant>> GetApprovePar()
        {
            if (_appDbContext.Participants is not null)
                return await _appDbContext.Participants.Where(p => p.IsApproved == true).ToListAsync();
            else
                return new List<Participant>();
        }

        public async Task<bool> UpdateParStatusAsync(string email, bool status)
        {
            var par = await _appDbContext.Participants.FirstOrDefaultAsync(p => p.Email == email);
            if (par != null)
            {
                par.IsApproved = status;
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemovePar(string email)
        {
            var par = await _appDbContext.Participants.FirstOrDefaultAsync(p => p.Email == email);
            if (par != null)
            {
                _appDbContext.Participants.Remove(par);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Participant> GetPar(string email)
        {
            var par = await _appDbContext.Participants.FirstOrDefaultAsync(p => p.Email == email);
            if (par != null)
                return par;
            else
                return null;
        }
    }
}
