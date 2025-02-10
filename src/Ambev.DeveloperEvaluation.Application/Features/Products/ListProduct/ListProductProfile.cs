using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.ListProduct;

/// <summary>
/// Profile for mapping between User entity and ListProductResponse
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProduct operation
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<Product, ListProductResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ListProductRateResult
            {
                Rate = src.Rate,
                Count = src.Count
            }));
    }
}
