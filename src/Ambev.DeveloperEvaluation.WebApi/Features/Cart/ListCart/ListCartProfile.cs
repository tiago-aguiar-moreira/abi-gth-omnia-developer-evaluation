using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCart;

/// <summary>
/// Profile for mapping ListCarts feature requests to commands
/// </summary>
public class ListCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCarts feature
    /// </summary>
    public ListCartProfile()
    {
        CreateMap<ListCartRequest, ListCartCommand>();
    }
}
