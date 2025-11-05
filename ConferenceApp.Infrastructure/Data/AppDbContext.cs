using ConferenceApp.Domain.Common;
using ConferenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ParOrgComm> ParOrgComms { get; set; }
        public DbSet<ParProgramComm> ParProgramComms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<DomainEvent>();

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FullName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Organization)
                    .HasMaxLength(100);

                entity.OwnsOne(p => p.Email, email =>
                {
                    email.Property(e => e.Value)
                        .HasColumnName("Email")
                        .HasMaxLength(150)
                        .IsRequired();
                });

                entity.Property(p => p.TitleLecture)
                    .HasMaxLength(200);

                entity.OwnsOne(p => p.Password, password =>
                {
                    password.Property(p => p.Hash)
                        .HasColumnName("PasswordHash")
                        .HasMaxLength(255)
                        .IsRequired();
                });

                entity.Property(p => p.Section)
                    .HasMaxLength(50);

                entity.Property(p => p.Role)
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.OwnsOne(p => p.ApplicationFile, file =>
                {
                    file.Property(f => f.Content)
                        .HasColumnName("ApplicationFileContent")
                        .HasColumnType("BLOB");

                    file.Property(f => f.FileName)
                        .HasColumnName("ApplicationFileName")
                        .HasMaxLength(255);

                    file.Property(f => f.ContentType)
                        .HasColumnName("ApplicationFileContentType")
                        .HasMaxLength(100);
                });

                entity.OwnsOne(p => p.ArticleFile, file =>
                {
                    file.Property(f => f.Content)
                        .HasColumnName("ArticleFileContent")
                        .HasColumnType("BLOB");

                    file.Property(f => f.FileName)
                        .HasColumnName("ArticleFileName")
                        .HasMaxLength(255);

                    file.Property(f => f.ContentType)
                        .HasColumnName("ArticleFileContentType")
                        .HasMaxLength(100);
                });

            });

            modelBuilder.Entity<Conference>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Title)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(c => c.Description)
                    .HasMaxLength(1000);

                entity.Property(c => c.Date)
                    .IsRequired()
                    .HasColumnType("DateTime");

                entity.HasMany(c => c.Participants)
                    .WithMany(); 
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.OwnsOne(a => a.Email, email =>
                {
                    email.Property(e => e.Value)
                        .HasColumnName("Email")
                        .HasMaxLength(150)
                        .IsRequired();
                });

                entity.OwnsOne(a => a.Password, password =>
                {
                    password.Property(p => p.Hash)
                        .HasColumnName("PasswordHash")
                        .HasMaxLength(255)
                        .IsRequired();
                });
            });

            modelBuilder.Entity<ParOrgComm>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FullName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Pos)
                    .HasMaxLength(100);

                entity.Property(p => p.Affiliation)
                    .HasMaxLength(100);

                entity.Property(p => p.Role)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ParProgramComm>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FullName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Pos)
                    .HasMaxLength(100);

                entity.Property(p => p.Affiliation)
                    .HasMaxLength(100);

                entity.Property(p => p.Role)
                    .HasMaxLength(50);
            });
        }
    }
}
