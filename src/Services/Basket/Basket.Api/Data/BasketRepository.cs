using Basket.Api.Exceptions;
using Basket.Api.Models;
using Marten;

namespace Basket.Api.Data
{
    public class BasketRepository(IDocumentSession session)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return cart is null ? throw new BasketNotFoundException(userName) : cart;    
        }
        public async Task<ShoppingCart> AddAsync(ShoppingCart card, CancellationToken cancellationToken = default)
        {
            session.Store(card);
            await session.SaveChangesAsync(cancellationToken);
            return card;
        }

        public async Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync();
            return true;
        }

    }
}
