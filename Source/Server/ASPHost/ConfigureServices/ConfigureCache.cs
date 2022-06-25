using HostData.Cache.Credentials;
using HostData.Cache.Licence;
using HostData.Cache.Orders;

namespace ASPHost.ConfigureServices;

public static class ConfigureCache
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddSingleton<ICredentialsCache, CredentialsCache>();
        services.AddSingleton<ISessionCache, SessionCache>();
        services.AddSingleton<ILicenceCache, LicenceCache>();
    }
}