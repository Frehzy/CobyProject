using Api.Factory.InternalModel;
using Microsoft.Extensions.Configuration;

namespace Api.Configuration;

public static class ConfigBuilder
{
    public static IConfigSettings Create()
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