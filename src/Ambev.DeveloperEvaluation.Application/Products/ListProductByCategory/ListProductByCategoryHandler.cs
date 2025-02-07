using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductByCategory;
public class ListProductByCategoryHandler : IRequestHandler<ListProductByCategoryCommand, PaginatedList<ListProductByCategoryResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductByCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListProductByCategoryResult>> Handle(ListProductByCategoryCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductByCategoryValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = await _productRepository.ListByCategoryAsync(
            command.CategoryName,
            command.PageNumber,
            command.PageSize,
            command.Order,
            cancellationToken);

        return new PaginatedList<ListProductByCategoryResult>(
            _mapper.Map<List<ListProductByCategoryResult>>(products),
            products.TotalCount,
            products.CurrentPage,
            products.PageSize
        );
    }
}
