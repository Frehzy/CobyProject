using SharedData.Mapper;

namespace LicenceServices.ConfigureServices;

public static class ConfigureMapper
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<IMapper, Mapper>();
    }
}