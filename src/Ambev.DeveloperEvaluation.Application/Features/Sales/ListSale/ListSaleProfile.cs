using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;

/// <summary>
/// Profile for mapping between User entity and ListSaleResponse
/// </summary>
public class ListSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSale operation
    /// </summary>
    public ListSaleProfile()
    {
        CreateMap<Sale, ListSaleResult>();
        CreateMap<SaleItem, ListSaleItemResult>();
    }
}
