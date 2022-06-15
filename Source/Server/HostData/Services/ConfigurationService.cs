using Shared.Configuration;
using Shared.Data;

namespace HostData.Services;

public class ConfigurationService : IConfigSettings
{
    private Guid _organizationId;

    public Guid OrganizationId => _organizationId;

    public ConfigurationService()
    {
        Update();
    }

    public void Update()
    {
        var config = ConfigBuilder.Create();
        _organizationId = config.OrganizationId;
    }
}