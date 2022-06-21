using HostData.Cache.Credentials;
using HostData.Cache.Orders;

namespace ASPHost.ConfigureServices;

public static class ConfigureCache
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<ICredentialsCache, CredentialsCache>();
        services.AddTransient<ISessionCache, SessionCache>();
    }
}