using HostData.Controllers.LogFactory;
using Microsoft.Extensions.Logging;
using Nancy;

namespace HostData.Modules;

public abstract class BaseModule : NancyModule
{
    private readonly ILogger _logger = Log.CreateLogger<BaseModule>();

    public BaseModule() : base("/") { }

    protected Response CreateExceptionResponse(string json, string typeException, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) =>
        new()
        {
            ContentType = "application/json",
            Contents = stream =>
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };
                writer.Write(json);
            },
            ReasonPhrase = typeException,
            StatusCode = statusCode
        };

    protected Response BadRequest(string exceptionMessage) =>
        new()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Contents = stream =>
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };
                writer.Write(exceptionMessage);
            },
        };
}