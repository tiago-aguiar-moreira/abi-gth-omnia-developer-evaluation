using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;

/// <summary>
/// Handler for processing ListSaleCommand requests
/// </summary>
public class ListSaleHandler : IRequestHandler<ListSaleCommand, PaginatedList<ListSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="ListSaleHandler"/>
    /// </summary>
    /// <param name="cartRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListSaleCommand request
    /// </summary>
    /// <param name="command">The ListSaleCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of sales if found, empty list if not found</returns>
    public async Task<PaginatedList<ListSaleResult>> Handle(ListSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var filters = new List<(string PropertyName, object? Value)>
        {
            (nameof(command.SaleNumber), command.SaleNumber),
            ("_" + nameof(command.MinSaleNumber), command.MinSaleNumber),
            ("_" + nameof(command.MaxSaleNumber), command.MaxSaleNumber),
            (nameof(command.SaleDate), command.SaleDate),
            ("_" + nameof(command.MinSaleDate), command.MinSaleDate),
            ("_" + nameof(command.MaxSaleDate), command.MaxSaleDate),
            (nameof(command.UserId), command.UserId),
            (nameof(command.TotalAmount), command.TotalAmount),
            (nameof(command.MinTotalAmount), command.MinTotalAmount),
            (nameof(command.MaxTotalAmount), command.MaxTotalAmount),
            (nameof(command.Branch), command.Branch),
            (nameof(command.Status), command.Status),
            (nameof(command.ProductId), command.ProductId),
            (nameof(command.Quantity), command.Quantity),
            ("_" + nameof(command.MinQuantity), command.MinQuantity),
            ("_" + nameof(command.MaxQuantity), command.MaxQuantity),
            (nameof(command.UnitPrice), command.UnitPrice),
            ("_" + nameof(command.MinUnitPrice), command.MinUnitPrice),
            ("_" + nameof(command.MaxUnitPrice), command.MaxUnitPrice),
            (nameof(command.Discount), command.Discount),
            ("_" + nameof(command.MinDiscount), command.MinDiscount),
            ("_" + nameof(command.MaxDiscount), command.MaxDiscount)
        };

        var sales = await _saleRepository.ListAsync(
            command.PageNumber,
            command.PageSize,
            command.Order,
            filters,
            cancellationToken);

        return new PaginatedList<ListSaleResult>(
            _mapper.Map<List<ListSaleResult>>(sales),
            sales.TotalItems,
            sales.CurrentPage,
            sales.PageSize
        );
    }
}
