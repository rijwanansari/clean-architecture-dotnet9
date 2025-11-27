using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Products;
using CleanArchitecture.Application.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Products.Commands.UpdateProductStock;
using CleanArchitecture.Application.Products.Commands.ActivateProduct;
using CleanArchitecture.Application.Products.Commands.DeactivateProduct;
using CleanArchitecture.Application.Products.Queries.GetProductById;
using CleanArchitecture.Application.Products.Queries.GetProducts;
using CleanArchitecture.Application.Products.Queries.GetProductsByCategory;
using CleanArchitecture.Application.Products.Queries.SearchProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

/// <summary>
/// Products API controller for managing product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get all products with pagination
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting products - Page: {Page}, PageSize: {PageSize}", page, pageSize);

        var query = new GetProductsQuery(page, pageSize);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to get products: {Error}", result.Error);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = result.Error });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Product details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductById(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting product by ID: {ProductId}", id);

        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("Product not found: {ProductId}", id);
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get products by category
    /// </summary>
    /// <param name="category">Category name</param>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of products in the category</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(PagedResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductsByCategory(
        string category,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting products by category: {Category}", category);

        var query = new GetProductsByCategoryQuery(category, page, pageSize);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to get products by category: {Error}", result.Error);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = result.Error });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Search products by keyword
    /// </summary>
    /// <param name="searchTerm">Search term to find products</param>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of matching products</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(PagedResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchProducts(
        [FromQuery] string searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new { error = "Search term is required" });
        }

        _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);

        var query = new SearchProductsQuery(searchTerm, page, pageSize);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to search products: {Error}", result.Error);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = result.Error });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="command">Product creation data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created product ID</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new product: {ProductName}", command.Name);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to create product: {Error}", result.Error);
            return BadRequest(new { error = result.Error, errors = result.Errors });
        }

        _logger.LogInformation("Product created successfully: {ProductId}", result.Data);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Data }, result.Data);
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="request">Product update data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success status</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(
        Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating product: {ProductId}", id);

        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.Currency,
            request.Category
        );

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to update product: {Error}", result.Error);
            
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { error = result.Error });

            return BadRequest(new { error = result.Error, errors = result.Errors });
        }

        _logger.LogInformation("Product updated successfully: {ProductId}", id);
        return NoContent();
    }

    /// <summary>
    /// Update product stock quantity
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="request">Stock update data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success status</returns>
    [HttpPatch("{id:guid}/stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProductStock(
        Guid id,
        [FromBody] UpdateStockRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating product stock: {ProductId}, Quantity: {Quantity}", id, request.Quantity);

        var command = new UpdateProductStockCommand(id, request.Quantity);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to update product stock: {Error}", result.Error);
            
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { error = result.Error });

            return BadRequest(new { error = result.Error });
        }

        _logger.LogInformation("Product stock updated successfully: {ProductId}", id);
        return NoContent();
    }

    /// <summary>
    /// Activate a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success status</returns>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActivateProduct(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Activating product: {ProductId}", id);

        var command = new ActivateProductCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to activate product: {Error}", result.Error);
            
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { error = result.Error });

            return BadRequest(new { error = result.Error });
        }

        _logger.LogInformation("Product activated successfully: {ProductId}", id);
        return NoContent();
    }

    /// <summary>
    /// Deactivate a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success status</returns>
    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeactivateProduct(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deactivating product: {ProductId}", id);

        var command = new DeactivateProductCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to deactivate product: {Error}", result.Error);
            
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { error = result.Error });

            return BadRequest(new { error = result.Error });
        }

        _logger.LogInformation("Product deactivated successfully: {ProductId}", id);
        return NoContent();
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting product: {ProductId}", id);

        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogError("Failed to delete product: {Error}", result.Error);
            
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { error = result.Error });

            return BadRequest(new { error = result.Error });
        }

        _logger.LogInformation("Product deleted successfully: {ProductId}", id);
        return NoContent();
    }
}

/// <summary>
/// Request model for updating product
/// </summary>
public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string Category
);

/// <summary>
/// Request model for updating product stock
/// </summary>
public record UpdateStockRequest(int Quantity);
