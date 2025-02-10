using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between User entity and UpdateCartResponse
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart operation
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartCommand, Cart>();
        CreateMap<UpdateCartItemCommand, CartItem>();

        CreateMap<Cart, UpdateCartResult>();
        CreateMap<CartItem, UpdateCartItemResult>();
    }
}
