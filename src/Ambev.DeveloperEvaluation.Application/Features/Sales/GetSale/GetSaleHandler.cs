﻿using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.GetSale;

/// <summary>
/// Handler for processing GetSaleCommand requests
/// </summary>
public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleCommand request
    /// </summary>
    /// <param name="command">The GetSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (cart == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        return _mapper.Map<GetSaleResult>(cart);
    }
}
