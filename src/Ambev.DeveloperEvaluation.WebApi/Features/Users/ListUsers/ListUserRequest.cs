using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// API response model for ListUsers operation
/// </summary>
public class ListUserRequest
{
    [FromQuery(Name = "_page")]
    public int? PageNumber { get; set; }

    [FromQuery(Name = "_size")]
    public int? PageSize { get; set; }

    [FromQuery(Name = "_order")]
    public string? Order { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public UserRole? Role { get; set; }

    public UserStatus? Status { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? Zipcode { get; set; }
    public decimal? Latitude { get; set; }

    [FromQuery(Name = "_minLatitude")]
    public decimal? MinLatitude { get; set; }

    [FromQuery(Name = "_maxLatitude")]
    public decimal? MaxLatitude { get; set; }
    public decimal? Longitude { get; set; }

    [FromQuery(Name = "_minLongitude")]
    public decimal? MinLongitude { get; set; }

    [FromQuery(Name = "_maxLongitude")]
    public decimal? MaxLongitude { get; set; }
}
