using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "participants.json");
        private List<Participant> _cache;

        private readonly AppDbContext _appDbContext;

        public ParticipantRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public ParticipantRepository()
        {
            if (File.Exists(Path.Combine(_filePath)))
            {
                var json = File.ReadAllText(_filePath);
                _cache = JsonSerializer.Deserialize<List<Participant>>(json) ?? new();
            }
            else
            {
                _cache = new List<Participant>();
            }
        }

        public async Task<List<Participant>> GetAll()
        {
            /*var json = File.ReadAllText(_filePath);
            var participants = JsonSerializer.Deserialize<List<Participant>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });*/

            if(_appDbContext.Participants is not null)
                return await _appDbContext.Participants.ToListAsync();
            else
                return _appDbContext.Participants.ToList() ?? new List<Participant>();
        }
        public async Task<List<Participant>> GetApprovedPar()
        {

            if (_appDbContext.Participants is not null)
                return await _appDbContext.Participants.Where(p => p.IsApproved == true).ToListAsync();
            else
                return _appDbContext.Participants.ToList() ?? new List<Participant>();
        }
        public async Task<Participant?> GetByCredentialsAsync(Guid id) =>
            await _appDbContext.Participants.FirstOrDefaultAsync(p => p.Id == id);
        public async Task<Participant?> GetByCredentialsAsync(string email) =>
            await _appDbContext.Participants.FirstOrDefaultAsync(p => p.Email == email);
        // for json
        //var user = _cache.FirstOrDefault(u => u.Email == email && u.Password == password);
        //return Task.FromResult(user);

        public Task UpdateAsync(string email, Participant updatedPart)
        {
            var index = _cache.FindIndex(u => u.Email == email);
            if (index != -1)
            {
                _cache[index] = updatedPart;
                SaveToFile();
            }
            return Task.CompletedTask;
        }
        public async Task UpdateAsync(Participant participant)
        {
            _appDbContext.Participants.Update(participant);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task SaveAsync(Participant participant)
        {
            _appDbContext.Participants.Add(participant);
            await _appDbContext.SaveChangesAsync();
            // for json
            //_cache.Add(participant);
            //SaveToFile();
            //return Task.CompletedTask;
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_cache, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public Task GetFileByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
