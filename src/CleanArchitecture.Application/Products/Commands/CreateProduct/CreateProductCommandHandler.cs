using System;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var price = Money.Of(request.Price, request.Currency);
            var product = Product.Create(
                request.Name,
                request.Description,
                price,
                request.StockQuantity,
                request.Category
            );

            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(product.Id);
        }
        catch (ArgumentException)
        {
            return Result<Guid>.Failure("Invalid product data provided.");
        }
        catch (InvalidOperationException)
        {
            return Result<Guid>.Failure("Operation could not be completed. Please check your request.");
        }
        catch (Exception)
        {
            // Optionally log the exception here
            return Result<Guid>.Failure("An unexpected error occurred. Please try again later.");
        }
    }
}
