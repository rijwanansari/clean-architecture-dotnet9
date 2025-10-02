using System;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            return Result<bool>.Failure($"Product with ID {request.Id} not found");

        try
        {
            var price = Money.Of(request.Price, request.Currency);
            product.UpdateDetails(request.Name, request.Description, price, request.Category);

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result<bool>.Failure("Invalid argument: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result<bool>.Failure("Operation could not be completed: " + ex.Message);
        }
        catch (Exception)
        {
            // Optionally log the exception here if a logger is available
            return Result<bool>.Failure("An unexpected error occurred. Please try again later.");
        }
    }

}
