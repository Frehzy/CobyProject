using Nancy;
using Nancy.Extensions;
using Serilog;
using Shared.Exceptions;
using System.Text.Json;

namespace HostData.Modules;

public abstract class BaseModule : NancyModule
{
    public BaseModule() : base("/") { }

    protected async Task<Response> Execute<T>(NancyContext context, Func<Task<T>> func)
    {
        try
        {
            Log.Information(CreateLogByContext(context));
            var result = await func();
            var returnJson = JsonSerializer.Serialize(result, System.Text.Json.Options.JsonSerializerOptions);
            Log.Information(CreateReturnLog(returnJson));
            return CreateGoodResponse(returnJson);
        }
        catch (EntityNotFoundException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(EntityNotFoundException));
        }
        catch (InvalidSessionException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(InvalidSessionException));
        }
        catch (PermissionDeniedException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(PermissionDeniedException));
        }
        catch (CantAddProductException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(CantAddProductException));
        }
        catch (CantChangeAndRemoveOrderException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(CantChangeAndRemoveOrderException));
        }
        catch (CantRemoveDeletedItemException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(CantRemoveDeletedItemException));
        }
        catch (WaiterDeletedOrPersonalSessionNotOpen ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(WaiterDeletedOrPersonalSessionNotOpen));
        }
        catch (EntityAlreadyExistsException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(EntityAlreadyExistsException));
        }
        catch(InvalidLicenceModuleException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(InvalidLicenceModuleException));
        }
        catch (EntityException ex)
        {
            var json = JsonSerializer.Serialize(ex.CreateDictionary(), System.Text.Json.Options.JsonSerializerOptions);
            Log.Error(ex, json);
            return CreateExceptionResponse(json, nameof(EntityException));
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return CreateBadResponse(ex.Message);
        }
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
            $"{JsonSerializer.Serialize(dic, System.Text.Json.Options.JsonSerializerOptions).Replace(@"\", string.Empty)}";
    }

    private string CreateReturnLog(string json) =>
        $"The server successfully processed the request. Server response to client:\n{json}";
}