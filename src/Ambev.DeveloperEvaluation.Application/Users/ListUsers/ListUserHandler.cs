using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Handler for processing ListUsersCommand requests
/// </summary>
public class ListUserHandler : IRequestHandler<ListUserCommand, PaginatedList<ListUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="ListUserHandler"/>
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    public ListUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListUsersCommand request
    /// </summary>
    /// <param name="command">The ListUsersCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of users if found, empty list if not found</returns>
    public async Task<PaginatedList<ListUserResult>> Handle(ListUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListUserValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var users = await _userRepository.ListAsync(
            command.PageNumber, command.PageSize, cancellationToken);

        return new PaginatedList<ListUserResult>(
            _mapper.Map<List<ListUserResult>>(users),
            users.TotalCount,
            users.CurrentPage,
            users.PageSize
        );
    }
}
