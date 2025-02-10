using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale;
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(ISaleRepository saleRepository, IProductService productService, IMapper mapper, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _productService = productService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);

        await _productService.CheckAndSetProductPriceAsync(sale.Products, cancellationToken);

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        if (updatedSale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        await _mediator.Publish(new SaleModifiedEvent(updatedSale), cancellationToken);

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }
}
