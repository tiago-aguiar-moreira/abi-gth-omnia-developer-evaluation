using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;
public class SaleCreatedEvent : INotification
{
    public Cart Cart { get; }

    public SaleCreatedEvent(Cart cart)
    {
        Cart = cart;
    }
}
