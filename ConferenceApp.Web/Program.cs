using ConferenceApp.Domain.Interfaces;
using ConferenceApp.Application.Interfaces;
using ConferenceApp.Application.Services;
using ConferenceApp.Web.Services;
using ConferenceApp.Infrastructure.Repositories;

namespace ConferenceApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IConferenceService, FakeConferenceService>();


            // DI для репозиториев и сервисов
            //builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();
            //builder.Services.AddScoped<IConferenceService, ConferenceService>();

            builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
            builder.Services.AddScoped<IParticipantService, ParticipantService>();
            builder.Services.AddScoped<IParProgramCommRepository, ParProgramCommRepository>();
            builder.Services.AddScoped<IParProgramCommService, ParProgramCommService>();
            builder.Services.AddScoped<IParOrgCommRepository, ParOrgCommRepository>();
            builder.Services.AddScoped<IParOrgCommService, ParOrgCommService>();

            // Настройка AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(name: "default",
                                   pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();

        }
    }
}
