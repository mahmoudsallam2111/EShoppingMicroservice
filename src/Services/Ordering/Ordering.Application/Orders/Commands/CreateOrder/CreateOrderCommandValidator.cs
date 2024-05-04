using FluentValidation;
using FluentValidation.Validators;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.Order.OrderName).NotEmpty().NotNull().WithMessage("order Name can not be empty or null");
            RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("customerId is required");
            RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("OrderItems Name can not be empty");
        }
    }
}
