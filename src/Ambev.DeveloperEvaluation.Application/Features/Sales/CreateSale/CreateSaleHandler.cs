using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSaleHandler(
        ISaleRepository saleRepository, IProductService productService, IMapper mapper, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _productService = productService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);

        await _productService.CheckAndSetProductPriceAsync(sale.Products, cancellationToken);

        var createSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        if (createSale == null)
            throw new Exception($"An error occurred while creating the cart with user id {sale.UserId}, sale numer {sale.SaleNumber} and date {sale.SaleDate}.");

        await _mediator.Publish(new SaleCreatedEvent(createSale), cancellationToken);

        return _mapper.Map<CreateSaleResult>(createSale);
    }
}
