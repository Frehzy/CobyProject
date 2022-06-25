using HostData.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureDatabase
{
    public static void ConfigureService(IServiceCollection services, DbContextOptions<ApiHostServicesDataContext> options)
    {
        services.AddSingleton(typeof(ApiHostServicesDataContext), _ => new ApiHostServicesDataContext(options));
    }
}