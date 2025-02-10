using Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Profile for mapping between Application and API UpdateSale responses
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale feature
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();

        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
            .ConstructUsing((src, ctx) => new UpdateSaleCommand
            {
                Id = ctx.Items["Id"] as Guid? ?? Guid.Empty,
                Products = ctx.Mapper.Map<List<UpdateSaleItemCommand>>(src.Products)
            }).ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
}
