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

    [FromQuery(Name = "username")]
    public string? Username { get; set; }

    [FromQuery(Name = "email")]
    public string? Email { get; set; }

    [FromQuery(Name = "phone")]
    public string? Phone { get; set; }

    [FromQuery(Name = "role")]
    public UserRole? Role { get; set; }

    [FromQuery(Name = "status")]
    public UserStatus? Status { get; set; }

    [FromQuery(Name = "city")]
    public string? City { get; set; }

    [FromQuery(Name = "street")]
    public string? Street { get; set; }

    [FromQuery(Name = "zipcode")]
    public string? Zipcode { get; set; }

    [FromQuery(Name = "_minLatitude")]
    public decimal? MinLatitude { get; set; }

    [FromQuery(Name = "_maxLatitude")]
    public decimal? MaxLatitude { get; set; }

    [FromQuery(Name = "_minLongitude")]
    public decimal? MinLongitude { get; set; }

    [FromQuery(Name = "_maxLongitude")]
    public decimal? MaxLongitude { get; set; }
}
