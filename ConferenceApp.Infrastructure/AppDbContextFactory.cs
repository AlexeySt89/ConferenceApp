using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConferenceApp.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Здесь укажи тот же connection string, что и в Program.cs
            optionsBuilder.UseSqlite("Data Source=conference.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
