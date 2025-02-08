using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;
public class SaleModifiedEvent : INotification
{
    public Cart Cart { get; }

    public SaleModifiedEvent(Cart cart)
    {
        Cart = cart;
    }
}
