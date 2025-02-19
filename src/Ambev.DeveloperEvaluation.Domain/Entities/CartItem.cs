﻿using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class CartItem : BaseEntity
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the date and time when the cart item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    public CartItem() => CreatedAt = DateTime.UtcNow;
}
