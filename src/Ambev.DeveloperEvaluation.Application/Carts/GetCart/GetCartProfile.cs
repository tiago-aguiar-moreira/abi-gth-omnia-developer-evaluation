using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Profile for mapping between User entity and GetProductResponse
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Cart, GetCartResult>();
        CreateMap<CartItem, GetCartItemResult>();
    }
}
