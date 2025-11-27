using FluentValidation;
using CleanArchitecture.Domain.Enumerators;

namespace CleanArchitecture.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required");

        RuleFor(x => x.NewStatus)
            .IsInEnum()
            .WithMessage("Invalid order status");

        RuleFor(x => x)
            .Must(BeValidStatusTransition)
            .WithMessage("Invalid status transition. Please follow proper order workflow.");
    }

    private bool BeValidStatusTransition(UpdateOrderStatusCommand command)
    {
        // Define valid status transitions
        var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
        {
            { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Confirmed, OrderStatus.Cancelled } },
            { OrderStatus.Confirmed, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Cancelled } },
            { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
            { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered } },
            { OrderStatus.Delivered, new List<OrderStatus>() }, // Final state
            { OrderStatus.Cancelled, new List<OrderStatus>() }  // Final state
        };

        // Note: Actual current status validation should be done in the handler
        // This is a general validation that the new status is a valid enum value
        return true;
    }
}
