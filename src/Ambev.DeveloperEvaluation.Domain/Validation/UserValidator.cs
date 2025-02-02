using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());

        RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

        RuleFor(user => user.Password).SetValidator(new PasswordValidator());

        RuleFor(user => user.Phone)
            .Matches(@"^\+[1-9]\d{10,14}$")
            .WithMessage("Phone number must start with '+' followed by 11-15 digits.");

        RuleFor(user => (UserStatus)user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("User status cannot be Unknown.");

        RuleFor(user => (UserRole)user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("User role cannot be None.");

        RuleFor(user => user.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City cannot be longer than 100 characters.");

        RuleFor(user => user.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(150).WithMessage("Street cannot be longer than 150 characters.");

        RuleFor(user => user.Number)
            .NotEmpty().WithMessage("Number is required.")
            .MaximumLength(20).WithMessage("Number cannot be longer than 20 characters.");

        RuleFor(user => user.Zipcode)
            .NotEmpty().WithMessage("Zip code is required.")
            .Matches(@"^\d{5}-\d{3}$").WithMessage("Zip code must follow the format '12345-678'.");

        RuleFor(user => user.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(user => user.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }
}
