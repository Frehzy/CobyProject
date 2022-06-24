using ApiModule.Operations;
using Shared.Data;

namespace ApiModule.Api;

public interface IConfiguration
{
    public IConfigSettings ConfigSettings { get; }

    public static IConfiguration CreateInstance() =>
        new Configuration();
}