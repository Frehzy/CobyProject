using Api.Model;
using FluentValidation;

namespace HostData.Validators;

internal class DinosaurValidator : AbstractValidator<Dinosaur>
{
    public DinosaurValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
    }
}
