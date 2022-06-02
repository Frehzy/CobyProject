using HostData.Model;
using Nancy;
using Nancy.ModelBinding;
using System.Text.Json;

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
        Get("/dinosaurs/{id}", parameters => { return JsonSerializer.Serialize(dinosaurs[parameters.id - 1]); });

        Post("/dinosaurs", parameters =>
        {
            var model = this.BindAndValidate<Dinosaur>();
            if (this.ModelValidationResult.IsValid is false)
                return 422;

            dinosaurs.Add(model);
            return dinosaurs.Count.ToString();
        });
    }
}
