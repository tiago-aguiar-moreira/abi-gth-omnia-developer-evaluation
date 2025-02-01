using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// API response model for ListUsers operation
/// </summary>
public class ListUsersResponse
{
    /// <summary>
    /// List of users
    /// </summary>
    public List<GetUserResponse> Users { get; set; } = [];
}