﻿using Shared.Configuration;

namespace HostData.Cache.Config;

internal class ConfigCache : IConfigCache
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