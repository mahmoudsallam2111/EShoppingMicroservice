using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Order.Id).NotNull().WithMessage("orderId can not be null");
            RuleFor(o => o.Order.OrderName).NotEmpty().NotNull().WithMessage("order Name can not be empty or null");
            RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("customerId is required");
            RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("OrderItems Name can not be empty");
        }
    }
}
