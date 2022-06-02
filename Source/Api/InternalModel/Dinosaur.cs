using Api.Data;

namespace Api.InternalModel;

internal class Dinosaur : IDinosaur
{
    public string Name { get; set; }

    public int HeightInFeet { get; set; }

    public string Status { get; set; }

    public Dinosaur() { }
}