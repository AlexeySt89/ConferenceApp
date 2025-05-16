using Microsoft.EntityFrameworkCore;
using ConferenceApp.Domain.Entities;
using System.Collections.Generic;

namespace ConferenceApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
