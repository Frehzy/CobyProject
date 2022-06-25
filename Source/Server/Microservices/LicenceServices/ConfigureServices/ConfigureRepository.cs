using LicenceData.Repository;

namespace LicenceServices.ConfigureServices;

public static class ConfigureRepository
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddScoped<ILicenceRepository, LicenceRepository>();
    }
}
