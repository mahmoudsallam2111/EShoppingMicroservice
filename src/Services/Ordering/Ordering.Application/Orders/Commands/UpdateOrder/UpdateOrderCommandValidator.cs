using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.OrderDto.Id).NotNull().WithMessage("orderId can not be null");
            RuleFor(o => o.OrderDto.OrderName).NotEmpty().NotNull().WithMessage("order Name can not be empty or null");
            RuleFor(o => o.OrderDto.CustomerId).NotNull().WithMessage("customerId is required");
            RuleFor(o => o.OrderDto.OrderItems).NotEmpty().WithMessage("OrderItems Name can not be empty");
        }
    }
}
