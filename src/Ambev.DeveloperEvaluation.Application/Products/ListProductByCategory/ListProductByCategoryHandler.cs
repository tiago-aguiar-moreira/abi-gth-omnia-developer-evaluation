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

        var filters = new List<(string PropertyName, object?)>
        {
            (nameof(command.Title), command.Title),
            (nameof(command.Price), command.Price),
            ("_" + nameof(command.MinPrice), command.MinPrice),
            ("_" + nameof(command.MaxPrice), command.MaxPrice),
            (nameof(command.Description), command.Description),
            (nameof(command.Image), command.Image),
            (nameof(command.Rate), command.Rate),
            ("_" + nameof(command.MinRate), command.MinRate),
            ("_" + nameof(command.MaxRate), command.MaxRate),
            (nameof(command.Count), command.Count),
            ("_" + nameof(command.MinCount), command.MinCount),
            ("_" + nameof(command.MaxCount), command.MaxCount)
        };

        var products = await _productRepository.ListByCategoryAsync(
            command.CategoryName,
            command.PageNumber,
            command.PageSize,
            command.Order,
            filters,
            cancellationToken);

        return new PaginatedList<ListProductByCategoryResult>(
            _mapper.Map<List<ListProductByCategoryResult>>(products),
            products.TotalCount,
            products.CurrentPage,
            products.PageSize
        );
    }
}
