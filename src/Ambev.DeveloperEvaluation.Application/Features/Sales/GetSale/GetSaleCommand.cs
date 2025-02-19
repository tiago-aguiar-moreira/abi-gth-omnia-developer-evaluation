﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.GetSale;
public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// The unique identifier of the sale to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
