namespace Meals.Validators;

public class MealValidator : AbstractValidator<Meal>
{
    public MealValidator()
    {
        RuleFor(m => m.MealName).NotEmpty().WithMessage("Verplicht een recept naam in te vullen!");
        RuleFor(m => m.MealInstructions).NotEmpty().MinimumLength(20).WithMessage("Minsens 20 karakters");
        RuleFor(m => m.MealArea).NotEmpty().WithMessage("Verplicht een area toe te voegen");
        RuleFor(m => m.MealCategory).NotEmpty().WithMessage("Verplicht een category toe te voegen");
    }
}