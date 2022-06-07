using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Extensions;

namespace HostData.Controllers.LogFactory;

public static class Log
{
    public static ILoggerFactory LoggerFactory { get; set; }
    public static ILogger CreateLogger<t>() => LoggerFactory.CreateLogger<t>();
    public static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);

    public static string CreateLog(NancyContext context)
    {
        var time = DateTime.Now;
        var url = context.Request.Url;
        var method = context.Request.Method;
        var protocolVersion = context.Request.ProtocolVersion;
        var body = context.Request.Body.AsString();

        var resultString = $"DateTime: {time}\n" +
                           $"Url: {url}\n" +
                           $"Method: {method}\n" +
                           $"ProtocolVersion: {protocolVersion}\n" +
                           $"Body: {body}\n";

        return resultString;
    }
}