using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class ConfigSettings : IConfigSettings
{
    public Guid OrganizationId { get; set; }

    public ConfigSettings() { }
}