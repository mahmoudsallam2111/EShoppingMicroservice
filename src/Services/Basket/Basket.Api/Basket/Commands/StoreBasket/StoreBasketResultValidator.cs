using FluentValidation;

namespace Basket.Api.Basket.Commands.StoreBasket
{
    public class StoreBasketResultValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketResultValidator()
        {
            RuleFor(s => s.Cart).NotEmpty().WithMessage("Card Can not Br Empty");
            RuleFor(s => s.Cart.UserName).NotEmpty().WithMessage("User name Can not Br Empty");
        }
    }
}
