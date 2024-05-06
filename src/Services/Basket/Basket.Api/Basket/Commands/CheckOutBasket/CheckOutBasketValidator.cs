using FluentValidation;

namespace Basket.Api.Basket.Commands.CheckOutBasket
{
    public class CheckOutBasketValidator : AbstractValidator<CheckOutBasketCommand>
    {
        public CheckOutBasketValidator()
        {
            RuleFor(c => c.BasketCheckOutDto).NotEmpty().WithMessage("BasketCheckOutDto is required!");
            RuleFor(c => c.BasketCheckOutDto.UserName).NotEmpty().WithMessage("UserName is required!");
        }
    }
}
