using Microsoft.EntityFrameworkCore;
using ConferenceApp.Domain.Entities;
using System.Collections.Generic;

namespace ConferenceApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<Conference> Conferences { get; set; }
    }
}
