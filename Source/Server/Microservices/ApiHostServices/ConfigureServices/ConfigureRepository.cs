using HostData.Domain.Repository;
using HostData.Repository;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureRepository
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddScoped<IDbRepository, DbRepository>();
    }
}
