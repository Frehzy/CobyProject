using ApiHostData.Cache.Credentials;
using ApiHostData.Cache.Licence;
using ApiHostData.Cache.Session;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureCache
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddSingleton<ICredentialsCache, CredentialsCache>();
        services.AddSingleton<ISessionCache, SessionCache>();
        services.AddSingleton<ILicenceCache, LicenceCache>();
    }
}