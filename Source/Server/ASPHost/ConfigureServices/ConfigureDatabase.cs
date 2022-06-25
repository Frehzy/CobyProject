using HostData.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace ASPHost.ConfigureServices;

public static class ConfigureDatabase
{
    public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
        DbContextOptionsBuilder<DataContext> options = new();
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly("ASPHost"));

        services.AddSingleton(typeof(DataContext), _ = new DataContext(options.Options));
    }
}