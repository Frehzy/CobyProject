using Shared.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Api.Http;

internal static class HttpRequest
{
    public static async Task<Response<T>> Get<T>(Uri uri)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(uri);
        if (response.StatusCode is not HttpStatusCode.OK)
            SwitchException(response);

        var json = await response.Content.ReadAsStringAsync();
        var instance = JsonSerializer.Deserialize<T>(json);
        return new Response<T>(response.StatusCode, uri, instance);
    }

    public static async Task<Response<T>> Post<T>(Uri uri, T instance)
    {
        using var client = new HttpClient();
        string json = JsonSerializer.Serialize(instance);
        var response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        if (response.StatusCode is not HttpStatusCode.OK)
            SwitchException(response);

        var instanceJson = await response.Content.ReadAsStringAsync();
        var responseInstance = JsonSerializer.Deserialize<T>(instanceJson);
        return new Response<T>(response.StatusCode, uri, responseInstance);
    }

    public static async Task<Response<TOut>> Post<TIn, TOut>(Uri uri, TIn instance)
    {
        using var client = new HttpClient();
        string json = JsonSerializer.Serialize(instance);
        var response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        if (response.StatusCode is not HttpStatusCode.OK)
            SwitchException(response);

        var instanceJson = await response.Content.ReadAsStringAsync();
        var responseInstance = JsonSerializer.Deserialize<TOut>(instanceJson);
        return new Response<TOut>(response.StatusCode, uri, responseInstance);
    }

    private static void SwitchException(HttpResponseMessage response)
    {
        var json = response.Content.ReadAsStringAsync().Result;
        throw response.ReasonPhrase switch
        {
            nameof(EntityNotFoundException) => JsonSerializer.Deserialize<EntityNotFoundException>(json),
            nameof(InvalidSessionException) => JsonSerializer.Deserialize<InvalidSessionException>(json),
            nameof(PermissionDeniedException) => JsonSerializer.Deserialize<PermissionDeniedException>(json),
            nameof(CantAddProductException) => JsonSerializer.Deserialize<CantAddProductException>(json),
            nameof(CantChangeAndRemoveOrderException) => JsonSerializer.Deserialize<CantChangeAndRemoveOrderException>(json),
            nameof(CantRemoveDeletedItemException) => JsonSerializer.Deserialize<CantRemoveDeletedItemException>(json),
            nameof(WaiterDeletedOrPersonalSessionNotOpen) => JsonSerializer.Deserialize<WaiterDeletedOrPersonalSessionNotOpen>(json),
            nameof(EntityAlreadyExistsException) => JsonSerializer.Deserialize<EntityAlreadyExistsException>(json),
            nameof(EntityException) => JsonSerializer.Deserialize<EntityException>(json),
            _ => new Exception(json),
        };
    }
}