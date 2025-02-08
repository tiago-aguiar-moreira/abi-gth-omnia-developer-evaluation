using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;
public class SaleCancelledEvent : INotification
{
    public Cart Cart { get; }

    public SaleCancelledEvent(Cart cart)
    {
        Cart = cart;
    }
}
