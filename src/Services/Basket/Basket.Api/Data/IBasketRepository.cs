using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetAsync(string userName , CancellationToken cancellationToken = default);
        Task<ShoppingCart> AddAsync(ShoppingCart card , CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default);

    }
}
