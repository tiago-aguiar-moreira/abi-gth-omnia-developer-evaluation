using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository, IProductService productService, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productService = productService;
        _mapper = mapper;
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

        return createSale == null
            ? throw new Exception($"An error occurred while creating the cart with user id {sale.UserId}, sale numer {sale.SaleNumber} and date {sale.SaleDate}.")
            : _mapper.Map<CreateSaleResult>(createSale);
    }
}
