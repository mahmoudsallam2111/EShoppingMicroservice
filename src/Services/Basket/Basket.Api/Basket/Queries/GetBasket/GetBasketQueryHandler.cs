using Basket.Api.Models;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.Queries.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return  new GetBasketResult(new ShoppingCart("mahmoud"));
        }
    }
}
