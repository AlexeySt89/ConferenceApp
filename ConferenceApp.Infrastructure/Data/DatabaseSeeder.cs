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
                    "Международная конференция УУНИТ 2024",
                    "Ежегодная технологическая конференция, представляющая новейшие инновации",
                    DateTime.UtcNow.AddMonths(1));

                await context.Conferences.AddAsync(conference);
            }

            if (!context.ParOrgComms.Any())
            {
                var orgMembers = new[]
                {
                    new ParOrgComm("Петр Иванов", "Профессор", "Технологический университет", "Кафедра"),
                    new ParOrgComm("Клавдия Петрова", "Директор", "Технологические инновации", "Координатор")
                };

                await context.ParOrgComms.AddRangeAsync(orgMembers);
            }

            if (!context.ParProgramComms.Any())
            {
                var programMembers = new[]
                {
                    new ParProgramComm("Сергей Викторов", "Исследователь", "Исследовательская лаборатория ИИ", "Рецензент"),
                    new ParProgramComm("Мария Михайлова", "Старший научный сотрудник", "Институт науки о данных", "Председатель")
                };

                await context.ParProgramComms.AddRangeAsync(programMembers);
            }

            await context.SaveChangesAsync();
        }
    }
}
