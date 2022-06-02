using Api.Data;
using Api.HttpRequest;
using Api.InternalModel;
using Api.Operations;

namespace Api;

public static class Http
{
    public static IDinosaur Get(int id)
    {
        var ip = NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "dinosaurs", id);
        var result = HttpGet.Get<Dinosaur>(uri, "03541926-da58-4d36-a9df-c2e16dbf356d");
        return result.Content;
    }

    public static void Post(IDinosaur dinosaur)
    {
        var ip = NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "dinosaurs");
        var result = HttpPost.Post(uri, "03541926-da58-4d36-a9df-c2e16dbf356d", dinosaur);
    }
}