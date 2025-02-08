using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class DeleteUserResponse
{ 
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's full name
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public DeleteUserAddressResponse Address { get; set; } = new();

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole Role { get; set; }
}

/// <summary>
/// Represents address details for a user creation request.
/// </summary>
public class DeleteUserAddressResponse
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
    public DeleteUserGeolocationResponse Geolocation { get; set; } = new();
}

/// <summary>
/// Represents geolocation details for a user creation request.
/// </summary>
public class DeleteUserGeolocationResponse
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
