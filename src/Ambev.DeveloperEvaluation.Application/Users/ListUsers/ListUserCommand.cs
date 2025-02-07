using Ambev.DeveloperEvaluation.Common.Helpers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Command for retrieving a list of all users
/// </summary>
public class ListUserCommand : IRequest<PaginatedList<ListUserResult>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Order { get; set; }
}
