﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.DeleteCart;

/// <summary>
/// Command for deleting a cart
/// </summary>
public class DeleteCartCommand : IRequest<DeleteCartResult>
{
    /// <summary>
    /// The unique identifier of the cart to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteCartCommand
    /// </summary>
    /// <param name="id">The ID of the cart to delete</param>
    public DeleteCartCommand(Guid id) => Id = id;
}
