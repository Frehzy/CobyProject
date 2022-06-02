using FluentValidation;
using HostData.Model;

namespace HostData.Validators;

internal class DinosaurValidator : AbstractValidator<Dinosaur>
{
    public DinosaurValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
    }
}
