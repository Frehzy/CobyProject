using Shared.Configuration;
using Shared.Data;

namespace HostData.Cache;

internal class ConfigCache : IConfigSettings
{
    private Guid _organizationId;

    public Guid OrganizationId => _organizationId;

    public ConfigCache()
    {
        Update();
    }

    public void Update()
    {
        var config = ConfigBuilder.Create();
        _organizationId = config.OrganizationId;
    }
}