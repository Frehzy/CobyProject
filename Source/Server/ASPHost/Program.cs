using Api.Operations;
using System.Net;

namespace ASPHost;

public class Program
{
    public static void Main(string[] args)
    {
        var ip = NetOperation.GetLocalIPAddress();
        CreateHostBuilder(args, ip).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args, IPAddress ip) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                         .UseKestrel(x =>
                         {
                             x.AllowSynchronousIO = true;
                         })
                         .UseUrls($"http://{ip}:5050")
                         .UseStartup<Startup>();
           });
}