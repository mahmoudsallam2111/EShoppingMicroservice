using FluentValidation;

namespace Basket.Api.Basket.Commands.DeleteBasket
{
    public class DeleteBasketCommanValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommanValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("User Name Can not Be empty");
        }
    }
}
