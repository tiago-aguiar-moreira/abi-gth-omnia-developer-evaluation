using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.CreateCart;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateCartHandler
    /// </summary>
    /// <param name="userRepository">The cart repository</param>
    /// <param name="productService">The product service</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateCartHandler(
        ICartRepository cartRepository, IProductService productService, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productService = productService;
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

        var existingCart = await _cartRepository.GetByUserIdAndDateAsync(command.UserId, command.Date, cancellationToken);
        if (existingCart != null)
            throw new InvalidOperationException($"An cart with user id {command.UserId} and date {command.Date:dd/MM/yyyy} already exists");

        var cart = _mapper.Map<Cart>(command);

        await _productService.CheckProductAsync(cart.Products, cancellationToken);

        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);

        return createdCart == null
            ? throw new Exception($"An error occurred while creating the cart with user id {cart.UserId} and date {cart.Date}.")
            : _mapper.Map<CreateCartResult>(createdCart);
    }
}
