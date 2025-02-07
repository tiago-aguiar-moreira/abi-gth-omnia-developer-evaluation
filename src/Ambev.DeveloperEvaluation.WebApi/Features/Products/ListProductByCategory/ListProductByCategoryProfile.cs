using Ambev.DeveloperEvaluation.Application.Products.ListProductByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductByCategory;

/// <summary>
/// Profile for mapping ListProductByCategory feature requests to commands
/// </summary>
public class ListProductByCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProductByCategory feature
    /// </summary>
    public ListProductByCategoryProfile()
    {
        CreateMap<ListProductByCategoryRequest, ListProductByCategoryCommand>();
    }
}
