using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateCartHandler
    /// </summary>
    /// <param name="userRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateCartHandler(
        ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateCartCommand request
    /// </summary>
    /// <param name="command">The CreateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingCart = await _cartRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingCart != null)
            throw new InvalidOperationException($"Cart with sale number {command.SaleNumber} already exists");

        var cart = _mapper.Map<Cart>(command);

        var products = await _productRepository.ListAsync(
            cart.Products.Select(s => s.ProductId).Distinct(),
            cancellationToken);

        foreach (var product in cart.Products)
        {
            var selectedProduct = products.FirstOrDefault(f => f.Id == product.ProductId)
                ?? throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");
            
            product.UnitPrice = selectedProduct.Price;
        }

        var success = await _cartRepository.CreateAsync(cart, cancellationToken);
        if(!success)
            throw new Exception($"An error occurred while processing the order. SaleNumber: {cart.SaleNumber}, SaleDate: {cart.SaleDate}.");

        var createdCart = await _cartRepository.GetByIdAsync(cart.Id, cancellationToken);
        var result = _mapper.Map<CreateCartResult>(createdCart);

        return result;
    }
}
