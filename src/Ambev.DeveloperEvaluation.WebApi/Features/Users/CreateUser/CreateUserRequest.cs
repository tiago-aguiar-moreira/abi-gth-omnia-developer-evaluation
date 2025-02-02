using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the username. Must be unique and contain only valid characters.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password. Must meet security requirements.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial status of the user account.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public CreateUserAddressRequest Address { get; set; } = new();
}

/// <summary>
/// Represents address details for a user creation request.
/// </summary>
public class CreateUserAddressRequest
{
    /// <summary>
    /// Gets or sets the city where the user resides.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street name of the user's address.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the house or apartment number.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal code of the user's address.
    /// </summary>
    public string Zipcode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geolocation details of the user's address.
    /// </summary>
    public CreateUserGeolocationRequest Geolocation { get; set; } = new();
}

/// <summary>
/// Represents geolocation details for a user creation request.
/// </summary>
public class CreateUserGeolocationRequest
{
    /// <summary>
    /// Gets or sets the latitude coordinate of the address.
    /// </summary>
    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the address.
    /// </summary>
    [JsonPropertyName("long")]
    public decimal Longitude { get; set; }
}
