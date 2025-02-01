using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Command for retrieving a list of all users
/// </summary>
public record ListUsersCommand : IRequest<ListUsersResult>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="order"></param>
    public ListUsersCommand()
    {

    }
}
