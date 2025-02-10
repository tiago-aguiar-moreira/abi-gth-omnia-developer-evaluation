using Ambev.DeveloperEvaluation.Application.Features.Products.ListProduct;
using Ambev.DeveloperEvaluation.Common.Helpers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct;

/// <summary>
/// Profile for mapping ListProducts feature requests to commands
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProducts feature
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<ListProductRequest, ListProductCommand>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order.ParseSortingFields()));
    }
}
