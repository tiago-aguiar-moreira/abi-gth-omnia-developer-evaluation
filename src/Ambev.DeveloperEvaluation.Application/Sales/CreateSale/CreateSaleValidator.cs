using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.SaleNumber)
            .GreaterThan(0).WithMessage("SaleNumber must be greater than 0.");

        RuleFor(x => x.SaleDate)
            .NotEmpty().WithMessage("SaleDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("SaleDate cannot be in the future.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Branch)
            .MaximumLength(100).WithMessage("Branch must not exceed 100 characters.");

        RuleFor(x => x.Status)
            .Must(status => Enum.IsDefined(typeof(SaleStatus), (short)status))
            .WithMessage("Status must be a valid SaleStatus value.");

        RuleFor(x => x.Products)
            .NotEmpty().WithMessage("At least one product must be included in the sale.");

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            product.RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        });
    }
}
