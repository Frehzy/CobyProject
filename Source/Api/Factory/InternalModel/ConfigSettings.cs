using Api.Configuration;

namespace Api.Factory.InternalModel;

internal class ConfigSettings : IConfigSettings
{
    public Guid OrganizationId { get; set; }

    public ConfigSettings() { }
}