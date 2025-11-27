using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Orders;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDetailsDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderDetailsDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        if (order == null)
            return Result<OrderDetailsDto>.Failure($"Order with ID {request.Id} not found");

        var items = order.Items.Select(item => new OrderItemDetailsDto(
            item.Id,
            item.ProductId,
            item.ProductName,
            item.Quantity,
            new MoneyDto(item.UnitPrice.Amount, item.UnitPrice.Currency),
            new MoneyDto(item.GetSubtotal().Amount, item.GetSubtotal().Currency)
        )).ToList();

        var dto = new OrderDetailsDto(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            order.Customer?.GetFullName() ?? string.Empty,
            order.Customer?.Email.Value ?? string.Empty,
            order.Status,
            order.PaymentMethod,
            new AddressDto(
                order.ShippingAddress.Street,
                order.ShippingAddress.City,
                order.ShippingAddress.State,
                order.ShippingAddress.ZipCode,
                order.ShippingAddress.Country
            ),
            items,
            new MoneyDto(order.GetTotalAmount().Amount, order.GetTotalAmount().Currency),
            order.CreatedAt,
            order.UpdatedAt
        );

        return Result<OrderDetailsDto>.Success(dto);
    }
}
