using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Enumerators;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result<bool>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdWithDetailsAsync(request.OrderId, cancellationToken);
            if (order == null)
                return Result<bool>.Failure($"Order with ID {request.OrderId} not found");

            // Check if order can be cancelled
            if (order.Status == OrderStatus.Delivered)
                return Result<bool>.Failure("Cannot cancel a delivered order");

            if (order.Status == OrderStatus.Cancelled)
                return Result<bool>.Failure("Order is already cancelled");

            if (order.Status == OrderStatus.Shipped)
                return Result<bool>.Failure("Cannot cancel an order that has been shipped");

            // Return stock to inventory
            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product != null)
                {
                    product.UpdateStock(item.Quantity); // Add back to stock
                }
            }

            order.Cancel();
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }
}
