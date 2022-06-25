using HostData.Controller.Contract;
using HostData.Controller.Implementation;

namespace LicenceServices.ConfigureServices;

public static class ConfigureController
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<ILicenceController, LicenceController>();
    }
}