using Microsoft.Extensions.Configuration;
using Shared.Factory.InternalModel;

namespace Shared.Configuration;

internal static class ConfigBuilder
{
    internal static ConfigSettings Create()
    {
        var config = new ConfigSettings();

        var filePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"Configuration\confgFile.json");

        ConfigurationBuilder builder = new();
        builder.AddJsonFile(filePath);
        IConfigurationRoot configuration = builder.Build();
        configuration.Bind(config);

        return config;
    }
}