using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using System.Text.Json;


namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParProgramCommRepository : IParProgramCommRepository
    {
        private readonly string _filePath;
        public ParProgramCommRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "ParProgramComm.json");
        }
        public List<ParProgramComm> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<ParProgramComm>();

            var json = File.ReadAllText(_filePath);
            var participants = JsonSerializer.Deserialize<List<ParProgramComm>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return participants ?? new List<ParProgramComm>();
        }
    }
}
