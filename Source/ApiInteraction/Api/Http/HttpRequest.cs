using Shared.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Api.Http;

internal static class HttpRequest
{
    public static Response<T> Get<T>(Uri uri)
    {
        using var client = new HttpClient();
        var response = client.GetAsync(uri).Result;
        if (response.StatusCode is not HttpStatusCode.OK)
            SwitchException(response);

        var json = response.Content.ReadAsStringAsync().Result;
        var instance = JsonSerializer.Deserialize<T>(json);
        return new Response<T>(response.StatusCode, uri, instance);
    }

    public static Response<T> Post<T>(Uri uri, T instance)
    {
        using var client = new HttpClient();
        string json = JsonSerializer.Serialize(instance);
        var response = client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        if (response.StatusCode is not HttpStatusCode.OK)
            SwitchException(response);

        var instanceJson = response.Content.ReadAsStringAsync().Result;
        var responseInstance = JsonSerializer.Deserialize<T>(instanceJson);
        return new Response<T>(response.StatusCode, uri, responseInstance);
    }

    public static Response<TOut> Post<TIn, TOut>(Uri uri, TIn instance)
    {
        using var client = new HttpClient();
        string json = JsonSerializer.Serialize(instance);
        var response = client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        if (response.StatusCode is not HttpStatusCode.OK)
            SwitchException(response);

        var instanceJson = response.Content.ReadAsStringAsync().Result;
        var responseInstance = JsonSerializer.Deserialize<TOut>(instanceJson);
        return new Response<TOut>(response.StatusCode, uri, responseInstance);
    }

    private static void SwitchException(HttpResponseMessage response)
    {
        var json = response.Content.ReadAsStringAsync().Result;
        throw response.ReasonPhrase switch
        {
            nameof(EntityException) => JsonSerializer.Deserialize<EntityException>(json),
            nameof(EntityNotFoundException) => JsonSerializer.Deserialize<EntityNotFoundException>(json),
            nameof(InvalidSessionException) => JsonSerializer.Deserialize<InvalidSessionException>(json),
            _ => new Exception(json),
        };
    }
}