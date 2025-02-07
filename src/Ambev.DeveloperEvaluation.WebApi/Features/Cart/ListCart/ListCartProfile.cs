using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.Common.Helpers;
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
        CreateMap<ListCartRequest, ListCartCommand>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order.ParseSortingFields()));
    }
}
