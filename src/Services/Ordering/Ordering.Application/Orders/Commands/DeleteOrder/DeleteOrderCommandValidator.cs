using FluentValidation;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(o => o.orderId).NotEmpty().WithMessage("OrderId can not be Empty");
        }
    }
}
