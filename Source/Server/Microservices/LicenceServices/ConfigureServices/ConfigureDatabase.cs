using LicenceData;
using Microsoft.EntityFrameworkCore;

namespace LicenceServices.ConfigureServices;

public static class ConfigureDatabase
{
    public static void ConfigureService(IServiceCollection services, DbContextOptions<LicenceServicesDataContext> options)
    {
        services.AddDbContext<LicenceServicesDataContext>(ServiceLifetime.Transient);
    }
}