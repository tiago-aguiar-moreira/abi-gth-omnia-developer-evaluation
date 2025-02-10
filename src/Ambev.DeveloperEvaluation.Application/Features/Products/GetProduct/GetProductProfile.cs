using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.GetProduct;

/// <summary>
/// Profile for mapping between User entity and GetProductResponse
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new GetProductRateResult
            {
                Rate = src.Rate,
                Count = src.Count
            }));
    }
}
