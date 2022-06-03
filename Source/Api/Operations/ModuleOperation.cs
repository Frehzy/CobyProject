using Api.Configuration;
using Api.Operations.Net;

namespace Api.Operations;

public sealed class ModuleOperation
{
    private static NetOperation _netOperation;
    private static IConfigSettings _configSettings;

    public static INetOperation NetOperation => _netOperation ??= new NetOperation();

    public static IConfigSettings ConfigSettings => _configSettings ??= ConfigBuilder.Create();
}