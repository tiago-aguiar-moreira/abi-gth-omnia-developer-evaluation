using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.GetProduct;
public class GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
