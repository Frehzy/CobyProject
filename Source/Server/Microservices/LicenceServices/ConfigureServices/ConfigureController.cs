using LicenceData.Controller.Contract;
using LicenceData.Controller.Implementation;

namespace LicenceServices.ConfigureServices;

public static class ConfigureController
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<ILicenceController, LicenceController>();
    }
}