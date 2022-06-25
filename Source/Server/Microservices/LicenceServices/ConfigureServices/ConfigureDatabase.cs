using HostData.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace LicenceServices.ConfigureServices;

public static class ConfigureDatabase
{
    public static void ConfigureService(IServiceCollection services, DbContextOptions<LicenceServicesDataContext> options)
    {
        services.AddSingleton(typeof(LicenceServicesDataContext), _ => new LicenceServicesDataContext(options));
    }
}