using ConferenceApp.Application.DTOs;
using ConferenceApp.Application.Interfaces;

namespace ConferenceApp.Web.Services
{
    public class FakeConferenceService : IConferenceService
    {
        private readonly List<ConferenceDto> _conferences = new()
        {
            new ConferenceDto
            {
                Id = Guid.NewGuid(),
                Title = "C# & .NET Conf 2025",
                Description = "Самая горячая конференция по .NET!",
                Date = new DateTime(2025, 6, 1)
            },
            new ConferenceDto
            {
                Id = Guid.NewGuid(),
                Title = "ASP.NET Core Summit",
                Description = "Все о Web API, Razor, Blazor",
                Date = new DateTime(2025, 7, 10)
            }
        };

        public Task<List<ConferenceDto>> GetConferencesAsync()
        {
            return Task.FromResult(_conferences);
        }

        public Task<ConferenceDto> GetConferenceByIdAsync(Guid id)
        {
            var result = _conferences.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(result!);
        }
    }
}