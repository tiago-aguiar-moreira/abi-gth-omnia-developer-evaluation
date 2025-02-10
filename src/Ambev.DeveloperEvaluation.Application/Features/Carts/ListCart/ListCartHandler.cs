using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.ListCart;

/// <summary>
/// Handler for processing ListCartCommand requests
/// </summary>
public class ListCartHandler : IRequestHandler<ListCartCommand, PaginatedList<ListCartResult>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="ListCartHandler"/>
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListCartHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListCartCommand request
    /// </summary>
    /// <param name="command">The ListCartCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of carts if found, empty list if not found</returns>
    public async Task<PaginatedList<ListCartResult>> Handle(ListCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var filters = new List<(string PropertyName, object? Value)>
        {
            (nameof(command.Date), command.Date),
            ("_" + nameof(command.MinDate), command.MinDate),
            ("_" + nameof(command.MaxDate), command.MaxDate),
            (nameof(command.UserId), command.UserId),
            (nameof(command.ProductId), command.ProductId),
            (nameof(command.Quantity), command.Quantity),
            ("_" + nameof(command.MinQuantity), command.MinQuantity),
            ("_" + nameof(command.MaxQuantity), command.MaxQuantity)
        };

        var carts = await _cartRepository.ListAsync(
            command.PageNumber,
            command.PageSize,
            command.Order,
            filters,
            cancellationToken);

        return new PaginatedList<ListCartResult>(
            _mapper.Map<List<ListCartResult>>(carts),
            carts.TotalItems,
            carts.CurrentPage,
            carts.PageSize
        );
    }

}
