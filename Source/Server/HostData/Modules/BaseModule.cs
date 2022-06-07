using Nancy;
using Nancy.Extensions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HostData.Modules;

public abstract class BaseModule : NancyModule
{
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

    protected string CreateLogByContext(NancyContext context)
    {
        Dictionary<string, string> dic = new()
        {
            { nameof(Context.Request.Body), context.Request.Body.AsString() }
        };
        return $"The server received a request. Request body:\n" +
            $"{JsonSerializer.Serialize(dic, CreateSerializerOptions()).Replace(@"\", string.Empty)}";
    }

    protected string CreateReturnLog(string json) =>
        $"The server successfully processed the request. Server response to client:\n{json}";

    protected JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            AllowTrailingCommas = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
        return options;
    }
}