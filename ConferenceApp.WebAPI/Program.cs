using ConferenceApp.Application.Interfaces;
using ConferenceApp.Application.Services;
using ConferenceApp.Domain.Interfaces;
using ConferenceApp.Infrastructure;
using ConferenceApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConferenceApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. CORS должен быть ПЕРВЫМ!
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalAndNull", policy =>
                {
                    policy.WithOrigins(
                            "https://conferenceapp.somee.com",
                            "http://localhost:7092",
                            "null"  // Для локальных файлов
                          )
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

                // 2. Остальные сервисы
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=conference.db"));

            builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
            builder.Services.AddScoped<ParticipantService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<JwtService>();

            // 3. Аутентификация (для теста можно закомментировать)
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", policy =>
                    policy.RequireRole("admin"));
            });

            var app = builder.Build();

            // 4. Middleware pipeline (КРИТИЧЕСКИ ВАЖЕН ПОРЯДОК!)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            // 5. CORS должен быть здесь
            app.UseCors("AllowLocalAndNull");


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}