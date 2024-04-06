using Basket.Api.Data;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.Commands.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSucceded);

    public class DeleteBasketCommandHandler (IBasketRepository repository)
       : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.UserName, cancellationToken);
            //2-delete basket from cache

            return new DeleteBasketResult(true);
        }
    }
}
