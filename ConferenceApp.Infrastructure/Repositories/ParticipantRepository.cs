using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using System.Text.Json;

namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "participants.json");
        private List<Participant> _cache;

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

        public List<Participant> GetAll()
        {
            /*var json = File.ReadAllText(_filePath);
            var participants = JsonSerializer.Deserialize<List<Participant>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });*/

            return _cache ?? new List<Participant>();
        }
        public Task<Participant?> GetByCredentialsAsync(string email, string password)
        {
            var user = _cache.FirstOrDefault(u => u.Email == email && u.Password == password);
            return Task.FromResult(user);
        }

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

        public Task SaveAsync(Participant participant)
        {
            _cache.Add(participant);
            SaveToFile();
            return Task.CompletedTask;
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_cache, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
