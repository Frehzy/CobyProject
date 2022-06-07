using Shared.Configuration;
using System.Net;

namespace ASPHost;

public class Program
{
    public static void Main(string[] args)
    {
        var ip = new NetOperation().GetLocalIPAddress();
        var hostBuilder = Host.CreateDefaultBuilder(args);
        var host = ConfigureHostBuilder(hostBuilder, ip);
        host.Run();
    }

    public static IHost ConfigureHostBuilder(IHostBuilder hostBuilder, IPAddress ip) =>
        hostBuilder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                      .UseKestrel(x =>
                      {
                          x.AllowSynchronousIO = true;
                      })
                      .UseUrls($"http://{ip}:5050")
                      .UseStartup<Startup>();
        }).Build();
}