using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.ListCategory;

/// <summary>
/// Profile for mapping between User entity and ListCategory
/// </summary>
public class ListCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCategory operation
    /// </summary>
    public ListCategoryProfile()
    {
        CreateMap<List<string>, ListCategoryResult>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.ToArray()));
    }
}
