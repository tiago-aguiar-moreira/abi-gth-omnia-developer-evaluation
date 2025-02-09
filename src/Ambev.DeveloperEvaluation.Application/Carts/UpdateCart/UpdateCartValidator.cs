using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
internal class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartValidator()
    {
        RuleFor(cart => cart.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(cart => cart.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(cart => cart.Products)
            .NotEmpty().WithMessage("Cart must contain at least one item.");

        RuleForEach(cart => cart.Products).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });

        RuleFor(cart => cart.Products)
            .Must(products => products.GroupBy(p => p.ProductId).All(g => g.Count() == 1))
            .WithMessage("Duplicate products are not allowed in the cart.");
    }
}
