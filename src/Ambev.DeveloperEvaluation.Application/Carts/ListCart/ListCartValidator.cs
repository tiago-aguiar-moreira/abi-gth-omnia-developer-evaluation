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
            .When(x => x.PageNumber.HasValue);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot be greater than 100.")
            .When(x => x.PageSize.HasValue);
    }
}