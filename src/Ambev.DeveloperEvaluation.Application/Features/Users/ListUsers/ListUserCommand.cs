using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Users.ListUsers;

/// <summary>
/// Command for retrieving a list of all users
/// </summary>
public class ListUserCommand : IRequest<PaginatedList<ListUserResult>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public List<(string PropertyName, bool Ascendent)> Order { get; set; } = [];
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public UserRole? Role { get; set; }
    public UserStatus? Status { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Zipcode { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? MinLatitude { get; set; }
    public decimal? MaxLatitude { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? MinLongitude { get; set; }
    public decimal? MaxLongitude { get; set; }
}
