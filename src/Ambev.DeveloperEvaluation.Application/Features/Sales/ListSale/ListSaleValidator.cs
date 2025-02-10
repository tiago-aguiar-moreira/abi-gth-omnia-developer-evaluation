using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;

/// <summary>
/// Validator for ListSaleCommand
/// </summary>
public class ListSaleValidator : AbstractValidator<ListSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for ListSaleCommand
    /// </summary>
    public ListSaleValidator()
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
