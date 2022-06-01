using Api.Model;
using System;

namespace Api;

public static class Http
{
    public static Dinosaur Get(int id)
    {
        using (var client = new HttpClient())
        {
            var endPoint = new Uri($"https://localhost:5001/dinosaurs/1");
            var result = client.GetAsync(endPoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;
        }
        return default;
    }
}