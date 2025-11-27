using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands.UpdateProductStock;

public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, Result<bool>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductStockCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
                return Result<bool>.Failure($"Product with ID {request.ProductId} not found");

            product.UpdateStock(request.QuantityChange);
            
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }
}
