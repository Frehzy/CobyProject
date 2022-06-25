using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Shared.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace LicenceServices;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File(rollingInterval: RollingInterval.Day,
                          rollOnFileSizeLimit: true,
                          shared: true,
                          outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                          path: string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"Logs\LicenceServicesLog.log"),
                          encoding: Encoding.UTF8)
            .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        var ip = NetOperation.GetLocalIPAddress();
        var hostBuilder = Host.CreateDefaultBuilder(args);
        var host = ConfigureHostBuilder(hostBuilder, ip);
        host.Run();
        Log.Information($"Starting {nameof(LicenceServices)} host");
    }

    public static IHost ConfigureHostBuilder(IHostBuilder hostBuilder, IPAddress ip) =>
        hostBuilder.UseSerilog()
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                                 .UseKestrel(x =>
                                 {
                                     x.AllowSynchronousIO = true;
                                 })
                                 .UseUrls($"http://{ip}:5051")
                                 .UseStartup<Startup>();
                   }).Build();
}