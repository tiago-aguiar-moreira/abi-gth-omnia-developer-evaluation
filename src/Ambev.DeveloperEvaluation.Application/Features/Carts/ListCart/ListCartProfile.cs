using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.ListCart;

/// <summary>
/// Profile for mapping between User entity and ListCartResponse
/// </summary>
public class ListCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCart operation
    /// </summary>
    public ListCartProfile()
    {
        CreateMap<Cart, ListCartResult>();
        CreateMap<CartItem, ListCartItemResult>();
    }
}
