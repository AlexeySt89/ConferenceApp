using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Domain.Interfaces.Services;
using ConferenceApp.Infrastructure.Data;
using ConferenceApp.Infrastructure.Repositories;
using ConferenceApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IParOrgCommRepository, ParOrgCommRepository>();
            services.AddScoped<IParProgramCommRepository, ParProgramCommRepository>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            return services;
        }
    }
}
