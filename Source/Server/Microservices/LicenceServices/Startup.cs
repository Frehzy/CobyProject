using LicenceData;
using LicenceServices.ConfigureServices;
using Microsoft.EntityFrameworkCore;
using Nancy.Owin;

namespace LicenceServices;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        app.UseOwin(x => x.UseNancy());
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        DbContextOptionsBuilder<LicenceServicesDataContext> options = new();
        options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly(nameof(LicenceData)));

        ConfigureServiceBase.ConfigureService(services);
        ConfigureMapper.ConfigureService(services);
        ConfigureController.ConfigureService(services);
        ConfigureDatabase.ConfigureService(services, options.Options);
        ConfigureRepository.ConfigureService(services);
    }
}