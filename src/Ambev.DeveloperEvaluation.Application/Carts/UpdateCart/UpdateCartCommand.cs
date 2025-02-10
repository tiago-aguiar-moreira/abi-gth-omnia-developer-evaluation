using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating a cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a cart. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateCartResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateCartValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateCartCommand : IRequest<UpdateCartResult>
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// List of sold products.
    /// </summary>
    public List<UpdateCartItemCommand> Products { get; set; } = [];
}

public class UpdateCartItemCommand
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }
}