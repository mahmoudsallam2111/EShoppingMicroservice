using FluentValidation;
using FluentValidation.Validators;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.OrderDto.OrderName).NotEmpty().NotNull().WithMessage("order Name can not be empty or null");
            RuleFor(o => o.OrderDto.CustomerId).NotNull().WithMessage("customerId is required");
            RuleFor(o => o.OrderDto.OrderItems).NotEmpty().WithMessage("OrderItems Name can not be empty");
        }
    }
}
