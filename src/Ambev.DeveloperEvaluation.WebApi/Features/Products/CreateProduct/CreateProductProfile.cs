﻿using Ambev.DeveloperEvaluation.Application.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct feature
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));

        CreateMap<CreateProductResult, CreateProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new CreateProductRateResponse
            {
                Rate = src.Rate,
                Count = src.Count
            }));
    }
}
