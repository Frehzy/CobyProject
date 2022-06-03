using Api.Configuration;

namespace Api.InternalModel;

internal class ConfigSettings : IConfigSettings
{
    public Guid OrganizationId { get; set; }

    public ConfigSettings() { }
}