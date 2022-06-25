using ApiHostData;
using Microsoft.EntityFrameworkCore;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureDatabase
{
    public static void ConfigureService(IServiceCollection services, DbContextOptions<ApiHostServicesDataContext> options)
    {
        services.AddDbContext<ApiHostServicesDataContext>(ServiceLifetime.Transient);
    }
}