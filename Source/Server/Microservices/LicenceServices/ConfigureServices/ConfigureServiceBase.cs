using HostData.Domain.Contracts.Services;
using HostData.Services;

namespace LicenceServices.ConfigureServices;

public static class ConfigureServiceBase
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<ILicenceService, LicenceService>();
    }
}