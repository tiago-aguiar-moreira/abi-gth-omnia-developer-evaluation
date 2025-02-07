using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCart;

/// <summary>
/// Validator for ListCartRequest
/// </summary>
public class ListCartRequestValidator : AbstractValidator<ListCartRequest>
{
    /// <summary>
    /// Initializes validation rules for ListCartRequest
    /// </summary>
    public ListCartRequestValidator()
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
