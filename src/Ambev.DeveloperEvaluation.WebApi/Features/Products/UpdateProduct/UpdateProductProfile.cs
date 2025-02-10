using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Application and API UpdateUser responses
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct feature
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ConstructUsing((src, ctx) => new UpdateProductCommand()
            {
                Id = ctx.Items["Id"] as Guid? ?? Guid.Empty
            }).ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateProductRateRequest, UpdateProductRateCommand>();

        CreateMap<UpdateProductResult, UpdateProductResponse>();
        CreateMap<UpdateProductRateResult, UpdateProductRateResponse>();
    }
}
