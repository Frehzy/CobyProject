using HostData.Model;
using HostData.Serialization;
using Nancy;
using Nancy.Extensions;

namespace HostData.Modules;

public class DinosaurModule : NancyModule
{
    private static readonly List<Dinosaur> dinosaurs = new()
    {
        new Dinosaur() {
           Name = "Kierkegaard",
           HeightInFeet = 6,
           Status = "Inflated"
        }
    };

    public DinosaurModule() : base("/")
    {
        Get("/dinosaurs/{id}", parameters =>
        {
            return Json.Serialization<Dinosaur>(dinosaurs[parameters.id - 1], "03541926-da58-4d36-a9df-c2e16dbf356d");
        });

        Post("/dinosaurs", parameters =>
        {
            var json = this.Request.Body.AsString();
            var obj = Json.Deserialize<Dinosaur>(json, "03541926-da58-4d36-a9df-c2e16dbf356d");
            dinosaurs.Add(obj);
            return dinosaurs.Count.ToString();
        });
    }
}
