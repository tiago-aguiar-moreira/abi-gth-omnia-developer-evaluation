using Ambev.DeveloperEvaluation.Application.Features.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Application and API UpdateUser responses
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser feature
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>();

        CreateMap<UpdateCartRequest, UpdateCartCommand>()
            .ConstructUsing((src, ctx) => new UpdateCartCommand
            {
                Id = ctx.Items["Id"] as Guid? ?? Guid.Empty,
                Products = ctx.Mapper.Map<List<UpdateCartItemCommand>>(src.Products)
            }).ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateCartResult, UpdateCartItemResponse>();
        CreateMap<UpdateCartResult, UpdateCartResponse>();
    }
}
