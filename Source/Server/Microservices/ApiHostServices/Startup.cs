using ApiHostServices.ConfigureServices;
using HostData.Domain.Context;
using HostData.Hubs;
using Microsoft.EntityFrameworkCore;
using Nancy.Owin;

namespace ApiHostServices;

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

        app.UseEndpoints(endPoint =>
        {
            endPoint.MapHub<OrderHub>("/ordersNotification");
            endPoint.MapHub<WaiterHub>("/waitersNotification");
        });

        app.UseOwin(x => x.UseNancy());
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        DbContextOptionsBuilder<ApiHostServicesDataContext> options = new();
        options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly(nameof(ApiHostServices)));

        ConfigureServiceBase.ConfigureService(services);
        ConfigureMapper.ConfigureService(services);
        ConfigureController.ConfigureService(services);
        ConfigureCache.ConfigureService(services);
        ConfigureDatabase.ConfigureService(services, options.Options);
        ConfigureRepository.ConfigureService(services);

        services.AddSignalR(options => options.EnableDetailedErrors = true);
    }
}