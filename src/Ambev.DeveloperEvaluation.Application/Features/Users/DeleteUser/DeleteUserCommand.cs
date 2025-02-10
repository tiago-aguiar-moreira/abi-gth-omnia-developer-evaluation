using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Users.DeleteUser;

/// <summary>
/// Command for deleting a user
/// </summary>
public class DeleteUserCommand : IRequest<DeleteUserResult>
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; set; }
}
