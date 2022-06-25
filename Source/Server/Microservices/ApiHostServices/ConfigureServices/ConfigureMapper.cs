using HostData.Mapper;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureMapper
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<IMapper, Mapper>();
    }
}