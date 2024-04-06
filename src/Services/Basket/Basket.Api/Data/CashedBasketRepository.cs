using Basket.Api.Models;
using JasperFx.Core;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    public class CashedBasketRepository (IBasketRepository _repository , IDistributedCache _cache)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetAsync(string userName, CancellationToken cancellationToken = default)
        {
            // Attempt to retrieve the cached basket
            var cachedBasket = await _cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                try
                {
                    // Deserialize the cached basket
                    return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
                }
                catch (JsonException)
                {
                    throw;
                }
            }

            // Fetch the basket from the repository
            var basket = await _repository.GetAsync(userName, cancellationToken);

            // Cache the fetched basket
            await _cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> AddAsync(ShoppingCart card, CancellationToken cancellationToken = default)
        {
             await _repository.AddAsync(card, cancellationToken);

            // cache it
           await _cache.SetStringAsync(card.UserName , JsonSerializer.Serialize(card) , cancellationToken);

            return card;
        }

        public async Task<bool> DeleteAsync(string userName, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(userName, cancellationToken);

            // delete it from cache
            await _cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

    }
}
