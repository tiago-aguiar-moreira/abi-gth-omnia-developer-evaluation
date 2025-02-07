﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductByCategory;

/// <summary>
/// Profile for mapping between User entity and ListProductByCategory
/// </summary>
public class ListProductByCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProductByCategory operation
    /// </summary>
    public ListProductByCategoryProfile()
    {
        CreateMap<Product, ListProductByCategoryResult>();
    }
}
