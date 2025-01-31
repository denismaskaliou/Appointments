using Appointments.WebAPI.Application.Services;
using Appointments.WebAPI.Application.Validators;
using Appointments.WebAPI.Database;
using Appointments.WebAPI.Web.Converters;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Appointments.WebAPI;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter())
            );

        services.AddScoped<IAppointmentsSlotsService, AppointmentsSlotsService>();
        services.AddValidatorsFromAssemblyContaining<AvailableSlotsQueryDtoValidator>();
        services.AddDbContext<ApplicationDbContext>((options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging();
        });
    }

    public void Configure(IApplicationBuilder builder)
    {
        builder
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGet(
                    pattern: "/",
                    requestDelegate: async context => await context.Response.WriteAsync("Ok"));

                endpoints.MapControllers();
            });
    }
}