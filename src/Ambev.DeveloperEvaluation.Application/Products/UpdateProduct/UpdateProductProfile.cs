using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between User entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct operation
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>();
    }
}
