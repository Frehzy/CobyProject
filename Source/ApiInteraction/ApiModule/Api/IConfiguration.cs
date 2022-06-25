using ApiModule.Operations;
using Shared.Data;

namespace ApiModule.Api;

public interface IConfiguration
{
    public IConfigSettings ConfigurationSettings { get; }

    public static IConfiguration CreateInstance() =>
        new Configuration();
}