namespace Meals.Validators;

public class AreaValidator : AbstractValidator<Area>
{
    public AreaValidator()
    {
        RuleFor(a => a.AreaName).NotEmpty().WithMessage("Verplicht een naam in te vullen!");
        RuleFor(a => a.AreaContinent).NotEmpty().WithMessage("Verplicht een continent in te vullen!");
    }
}