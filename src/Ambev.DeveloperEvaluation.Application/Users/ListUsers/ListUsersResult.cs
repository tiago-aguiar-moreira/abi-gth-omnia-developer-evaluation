using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Response model for ListUsersResult operation
/// </summary>
public class ListUsersResult
{
    /// <summary>
    /// The list of the users
    /// </summary>
    public List<GetUserResult> Users { get; set; } = [];
}