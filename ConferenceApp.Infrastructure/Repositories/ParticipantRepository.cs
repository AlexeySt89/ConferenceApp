using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext _context;

        public ParticipantRepository(AppDbContext appDbContext) => _context = appDbContext;

        public async Task AddAsync(Participant participant)
        {
            await _context.AddAsync(participant);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var participant = await GetByIdAsync(id);
            if(participant == null) 
                return false;

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ExistsAsync(Email email)
        {
            return await _context.Participants
                .AnyAsync(p => p.Email.Value == email.Value);
        }

        public async Task<IReadOnlyList<Participant>> GetAllAsync()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<IReadOnlyList<Participant>> GetApprovedAsync()
        {
            return await _context.Participants.Where(x => x.IsApproved == true).ToListAsync();
        }

        public async Task<Participant?> GetByEmailAsync(Email email)
        {
            return await _context.Participants.FirstOrDefaultAsync(x => x.Email.Value == email.Value);
        }

        public async Task<Participant?> GetByIdAsync(Guid id)
        {
            return await _context.Participants.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Participant>> GetPendingAsync()
        {
            return await _context.Participants.Where(p => p.IsApproved == null).ToListAsync();
        }

        public async Task<bool> UpdateApprovalStatusAsync(Email email, bool isApproved, string? rejectReason = null)
        {
            var participant = await GetByEmailAsync(email);
            if (participant == null)
                return false;

            if(isApproved)
            {
                participant.Approve();
            }
            else
            {
                participant.Reject(rejectReason ?? "Reason not specified");
            }

            await UpdateAsync(participant);
            return true;
        }

        public async Task UpdateAsync(Participant participant)
        {
            _context.Participants.Update(participant);
            await _context.SaveChangesAsync();
        }
    }
}
