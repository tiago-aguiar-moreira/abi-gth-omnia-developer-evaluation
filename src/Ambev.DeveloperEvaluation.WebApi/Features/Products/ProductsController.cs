﻿using Ambev.DeveloperEvaluation.Application.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Features.Products.ListCategory;
using Ambev.DeveloperEvaluation.Application.Features.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Features.Products.ListProductByCategory;
using Ambev.DeveloperEvaluation.Application.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing product operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="request">The product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok("Product deleted successfully");
    }

    /// <summary>
    /// Retrieves a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var command = _mapper.Map<GetProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(
            _mapper.Map<GetProductResponse>(response),
            "Product retrieved successfully");
    }

    /// <summary>
    /// Updates a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product was updated</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var command = _mapper.Map<UpdateProductCommand>(request, opt => opt.Items["Id"] = id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<UpdateProductResponse>(response), "Product updated successfully");
    }

    /// <summary>
    /// Retrieves a list of products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products if found, empty list if not found</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListProducts(ListProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new ListProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var command = _mapper.Map<ListProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        
        return OkPaginated(response);
    }

    /// <summary>
    /// Retrieves a list of products by category name
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products if found, empty list if not found</returns>
    [HttpGet("category/{CategoryName}")]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListProductsByCategory(ListProductByCategoryRequest request, CancellationToken cancellationToken)
    {
        var validator = new ListProductByCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var command = _mapper.Map<ListProductByCategoryCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return OkPaginated(response);
    }

    /// <summary>
    /// Retrieves a list of product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of product categories if found, empty list if not found</returns>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListProductsByCategory(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListCategoryCommand(), cancellationToken);

        return Ok(response.Category, "Category retrieved successfully");
    }
}
