using ApiModule.Api;
using Shared.Data;
using Shared.Factory.InternalModel;

namespace ApiModule.Operations;

internal sealed class Configuration : IConfiguration
{
    private IConfigSettings _configSettings;

    public IConfigSettings ConfigurationSettings => _configSettings ??= ConfigSettings.CreateInstance();
}