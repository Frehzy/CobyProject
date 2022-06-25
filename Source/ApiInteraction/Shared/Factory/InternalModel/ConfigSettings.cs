using Shared.Configuration;
using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class ConfigSettings : IConfigSettings
{
    private static ConfigSettings _instance;

    public Guid OrganizationId { get; set; }

    public Guid TerminalId { get; private set; } = Guid.NewGuid();

    public static IConfigSettings CreateInstance() =>
        _instance ??= ConfigBuilder.Create();

    internal ConfigSettings() { }
}