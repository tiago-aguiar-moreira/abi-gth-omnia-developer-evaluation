using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Validation;

public class OrderParameterValidator : AbstractValidator<string?>
{
    private readonly string[] validSortOrders = ["asc", "desc"];

    public OrderParameterValidator()
    {
        RuleFor(order => order)
            .Must(BeValidOrderFormat)
            .WithMessage("The order parameter format is invalid. Example: 'price desc, title asc' or 'price desc, title'.");
    }

    private bool BeValidOrderFormat(string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return true;

        var parts = order
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToList();

        return parts.All(part => part.Length == 1 ||
            (part.Length == 2 && validSortOrders.Contains(part[1], StringComparer.OrdinalIgnoreCase)));
    }
}
