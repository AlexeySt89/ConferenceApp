using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Services;

namespace ConferenceApp.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IPasswordHasher passwordHasher)
        {
            if (!context.Admins.Any())
            {
                var admin = new Admin(
                    new Email("admin@conference.com"),
                    Password.CreateFromPlainText("admin123", passwordHasher));

                await context.Admins.AddAsync(admin);
            }

            if (!context.Conferences.Any())
            {
                var conference = new Conference(
                    "International Tech Conference 2024",
                    "Annual technology conference featuring the latest innovations",
                    DateTime.UtcNow.AddMonths(1));

                await context.Conferences.AddAsync(conference);
            }

            if (!context.ParOrgComms.Any())
            {
                var orgMembers = new[]
                {
                    new ParOrgComm("John Smith", "Professor", "University of Technology", "Chair"),
                    new ParOrgComm("Maria Garcia", "Director", "Tech Innovations Inc", "Coordinator")
                };

                await context.ParOrgComms.AddRangeAsync(orgMembers);
            }

            if (!context.ParProgramComms.Any())
            {
                var programMembers = new[]
                {
                    new ParProgramComm("Dr. Robert Chen", "Researcher", "AI Research Lab", "Reviewer"),
                    new ParProgramComm("Dr. Sarah Johnson", "Senior Scientist", "Data Science Institute", "Chair")
                };

                await context.ParProgramComms.AddRangeAsync(programMembers);
            }

            await context.SaveChangesAsync();
        }
    }
}
