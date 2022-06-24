using ApiModule.Api;
using Shared.Configuration;
using Shared.Data;

namespace ApiModule.Operations;

internal sealed class Configuration : IConfiguration
{
    private IConfigSettings _configSettings;

    public IConfigSettings ConfigSettings => _configSettings ??= ConfigBuilder.Create();
}