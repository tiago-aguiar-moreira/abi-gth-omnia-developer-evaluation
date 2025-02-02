using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Request model for updating a user
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number for the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public UpdateUserAddressRequest Address { get; set; } = new();
}

/// <summary>
/// Represents address details for a user creation request.
/// </summary>
public class UpdateUserAddressRequest
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
    public UpdateUserGeolocationRequest Geolocation { get; set; } = new();
}

/// <summary>
/// Represents geolocation details for a user creation request.
/// </summary>
public class UpdateUserGeolocationRequest
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
