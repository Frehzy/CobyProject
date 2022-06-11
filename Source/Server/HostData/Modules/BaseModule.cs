using Nancy;
using Nancy.Extensions;
using Serilog;
using Shared.Exceptions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HostData.Modules;

public abstract class BaseModule : NancyModule
{
    public BaseModule() : base("/") { }

    protected Response Execute<T>(NancyContext context, Func<Task<T>> func)
    {
        return Task.Run(async () =>
        {
            try
            {
                Log.Information(CreateLogByContext(context));
                var result = await func();
                var returnJson = JsonSerializer.Serialize(result);
                Log.Information(CreateReturnLog(returnJson));
                return CreateGoodResponse(returnJson);
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary(), CreateSerializerOptions());
                Log.Error(ex, json);
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (InvalidSessionException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary(), CreateSerializerOptions());
                Log.Error(ex, json);
                return CreateExceptionResponse(json, nameof(InvalidSessionException));
            }
            catch (EntityException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary(), CreateSerializerOptions());
                Log.Error(ex, json);
                return CreateExceptionResponse(json, nameof(EntityException));
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return CreateBadResponse(ex.Message);
            }
        }).Result;
    }

    private Response CreateGoodResponse(string json) =>
        new()
        {
            ContentType = "application/json",
            Contents = stream =>
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };
                writer.Write(json);
            },
            StatusCode = HttpStatusCode.OK
        };

    private Response CreateExceptionResponse(string json, string typeException, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) =>
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

    private Response CreateBadResponse(string exceptionMessage) =>
        new()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Contents = stream =>
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };
                writer.Write(exceptionMessage);
            },
        };

    private string CreateLogByContext(NancyContext context)
    {
        Dictionary<string, string> dic = new()
        {
            { nameof(Context.Request.Body), context.Request.Body.AsString() }
        };
        return $"The server received a request. Request body:\n" +
            $"{JsonSerializer.Serialize(dic, CreateSerializerOptions()).Replace(@"\", string.Empty)}";
    }

    private string CreateReturnLog(string json) =>
        $"The server successfully processed the request. Server response to client:\n{json}";

    private JsonSerializerOptions CreateSerializerOptions()
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