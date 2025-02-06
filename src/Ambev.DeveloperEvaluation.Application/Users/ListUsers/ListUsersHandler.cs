using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Handler for processing ListUsersCommand requests
/// </summary>
public class ListUsersHandler : IRequestHandler<ListUsersCommand, ListUsersResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListUsersHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListUsersHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListUsersCommand request
    /// </summary>
    /// <param name="command">The ListUsersCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The lis of users if found, empty list if not found</returns>
    public async Task<ListUsersResult> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListUsersValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var users = await _userRepository.ListAsync(cancellationToken);
        var result = _mapper.Map<ListUsersResult>(users);
        return result;
    }
}
