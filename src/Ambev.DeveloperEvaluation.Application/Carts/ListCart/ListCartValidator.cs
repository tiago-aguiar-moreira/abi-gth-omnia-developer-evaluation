using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart;

/// <summary>
/// Validator for ListCartCommand
/// </summary>
public class ListCartValidator : AbstractValidator<ListCartCommand>
{
    /// <summary>
    /// Initializes validation rules for ListCartCommand
    /// </summary>
    public ListCartValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.")
            .When(x => x.PageNumber.HasValue); // Só valida se PageNumber não for nulo

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot be greater than 100.")
            .When(x => x.PageSize.HasValue); // Só valida se PageSize não for nulo

        RuleFor(x => x.Order)
            .Matches(@"^(\w+\s(asc|desc))(,\s*\w+\s(asc|desc))*$") // Regex to validate "field1 asc, field2 desc"
            .When(x => !string.IsNullOrWhiteSpace(x.Order))
            .WithMessage("Sorting format must be: 'field1 asc, field2 desc'.");
    }
}