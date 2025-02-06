using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserCommand that defines validation rules for user update command.
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in a valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match the international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// - Address:
    ///   - City: Required, cannot be empty
    ///   - Street: Required, cannot be empty
    ///   - Number: Must be a valid integer
    ///   - Zipcode: Must match the postal code format
    ///   - Geolocation: 
    ///     - Latitude: Must be a valid coordinate (can be decimal or string, depending on the format)
    ///     - Longitude: Must be a valid coordinate (can be decimal or string, depending on the format)
    /// </remarks>
    public UpdateUserRequestValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);
        RuleFor(user => user.Address.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City cannot be longer than 100 characters.");

        RuleFor(user => user.Address.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(150).WithMessage("Street cannot be longer than 150 characters.");

        RuleFor(user => user.Address.Number)
            .NotEmpty()
            .WithMessage("Number is required.")
            .MaximumLength(20)
            .WithMessage("Number cannot be longer than 20 characters.");

        RuleFor(user => user.Address.Zipcode)
            .NotEmpty().WithMessage("Zip code is required.")
            .Matches(@"^\d{5}-\d{3}$")
            .WithMessage("Zip code must follow the format '12345-678'.");

        RuleFor(user => user.Address.Geolocation.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");

        RuleFor(user => user.Address.Geolocation.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");
    }
}
