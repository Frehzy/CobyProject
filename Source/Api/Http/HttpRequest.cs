using Api.Serialization;
using System.Net;
using System.Text;

namespace Api.Http;

internal static class HttpRequest
{
    public static Response<T> Get<T>(Uri uri, string key)
    {
        using var client = new HttpClient();
        var response = client.GetAsync(uri).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        if (response.StatusCode is not HttpStatusCode.OK)
            return new Response<T>(response.StatusCode, uri, default);

        var instance = Json.Deserialize<T>(json, key);
        return new Response<T>(response.StatusCode, uri, instance);
    }

    public static Response<T> Post<T>(Uri uri, string key, T instance)
    {
        using var client = new HttpClient();
        string json = Json.Serialization(instance, key);
        var response = client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        if (response.StatusCode is not HttpStatusCode.OK)
            return new Response<T>(response.StatusCode, uri, default);

        var instanceJson = response.Content.ReadAsStringAsync().Result;
        return new Response<T>(response.StatusCode, uri, Json.Deserialize<T>(instanceJson, key));
    }

    public static Response<TOut> Post<TIn, TOut>(Uri uri, string key, TIn instance)
    {
        using var client = new HttpClient();
        string json = Json.Serialization(instance, key);
        var response = client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        if (response.StatusCode is not HttpStatusCode.OK)
            return new Response<TOut>(response.StatusCode, uri, default);

        var instanceJson = response.Content.ReadAsStringAsync().Result;
        return new Response<TOut>(response.StatusCode, uri, Json.Deserialize<TOut>(instanceJson, key));
    }
}