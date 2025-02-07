using Ambev.DeveloperEvaluation.WebApi.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductByCategory;

/// <summary>
/// Validator for ListProductByCategoryRequest
/// </summary>
public class ListProductByCategoryRequestValidator : AbstractValidator<ListProductByCategoryRequest>
{
    /// <summary>
    /// Initializes validation rules for ListProductByCategoryRequest
    /// </summary>
    public ListProductByCategoryRequestValidator()
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

        RuleFor(x => x.Order).SetValidator(new OrderParameterValidator());
    }
}
