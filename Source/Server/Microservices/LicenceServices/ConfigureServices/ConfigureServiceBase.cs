using LicenceData.Services.Contract;
using LicenceData.Services.Implementation;

namespace LicenceServices.ConfigureServices;

public static class ConfigureServiceBase
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<ILicenceService, LicenceService>();
    }
}