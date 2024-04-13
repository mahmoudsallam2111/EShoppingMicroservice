using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;

namespace Basket.Api.Basket.Commands.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler (IBasketRepository repository , DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            //consume discount.grpc service and calculate discount
            await CalculateDiscount(request, cancellationToken);

            // insert or update in database
            var basket = await repository.AddAsync(request.Cart, cancellationToken);
            // update cache 

            return new StoreBasketResult(basket.UserName);
        }

        private  async Task CalculateDiscount(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Cart.Items)
            {
                var coupon = await discountProto
                    .GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);


                item.Price -= coupon.Amount;
            }
        }
    }
}
