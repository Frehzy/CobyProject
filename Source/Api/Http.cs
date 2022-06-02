using Api.Data;
using Api.InternalModel;
using Api.Operations;
using System.Net;
using System.Text.Json;

namespace Api;

public static class Http
{
    public static IDinosaur Get(int id)
    {
        var ip = NetOperation.GetLocalIPAddress();
        using var client = new HttpClient();

        var endPoint = new Uri($"http://{ip}:5050/dinosaurs/{id}");
        var result = client.GetAsync(endPoint).Result;
        var json = result.Content.ReadAsStringAsync().Result;

        return result.StatusCode is HttpStatusCode.OK
            ? JsonSerializer.Deserialize<Dinosaur>(json)
            : default;
    }
}