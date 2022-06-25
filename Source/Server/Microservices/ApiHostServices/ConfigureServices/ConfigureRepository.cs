﻿using HostData.Repository.Contracts;
using HostData.Repository.Implementation;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureRepository
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddScoped<IApiHostRepository, ApiHostRepository>();
    }
}
