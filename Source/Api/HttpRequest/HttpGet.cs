using Api.Serialization;
using System.Net;

namespace Api.HttpRequest;

internal static class HttpGet
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
}