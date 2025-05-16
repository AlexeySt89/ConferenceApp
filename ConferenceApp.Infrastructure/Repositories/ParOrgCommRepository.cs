using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using System.Text.Json;


namespace ConferenceApp.Infrastructure.Repositories
{
    public class ParOrgCommRepository : IParOrgCommRepository
    {
        private readonly string _filePath;
        public ParOrgCommRepository()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "ParOrgComm.json");
        }
        public List<ParOrgComm> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<ParOrgComm>();

            var json = File.ReadAllText(_filePath);
            var participants = JsonSerializer.Deserialize<List<ParOrgComm>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return participants ?? new List<ParOrgComm>();
        }
    }
}
