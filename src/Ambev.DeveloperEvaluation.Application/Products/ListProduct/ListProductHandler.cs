using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;
public class ListProductHandler : IRequestHandler<ListProductCommand, PaginatedList<ListProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListProductResult>> Handle(ListProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = await _productRepository.ListAsync(
            command.PageNumber, command.PageSize, cancellationToken);

        return new PaginatedList<ListProductResult>(
            _mapper.Map<List<ListProductResult>>(products),
            products.TotalCount,
            products.CurrentPage,
            products.PageSize
        );
    }
}