using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.ListCategory;
public class ListCategoryHandler : IRequestHandler<ListCategoryCommand, ListCategoryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListCategoryResult> Handle(ListCategoryCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.ListCategoryAsync(cancellationToken);

        return _mapper.Map<ListCategoryResult>(products);
    }
}
