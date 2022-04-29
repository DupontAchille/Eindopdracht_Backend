namespace Meals.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Verplicht een naam in te vullen!");
    }
}