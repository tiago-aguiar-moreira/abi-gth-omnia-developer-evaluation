using Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.Common.Helpers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;

/// <summary>
/// Profile for mapping ListSales feature requests to commands
/// </summary>
public class ListSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSales feature
    /// </summary>
    public ListSaleProfile()
    {
        CreateMap<ListSaleRequest, ListSaleCommand>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order.ParseSortingFields()));
    }
}
