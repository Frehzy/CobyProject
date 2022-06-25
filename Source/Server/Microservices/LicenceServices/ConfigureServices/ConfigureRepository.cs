using HostData.Repository.Contracts;
using HostData.Repository.Implementation;

namespace LicenceServices.ConfigureServices;

public static class ConfigureRepository
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddScoped<ILicenceRepository, LicenceRepository>();
    }
}
