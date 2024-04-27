using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    /// <summary>
    /// since we use clean arch,the application layer does not have a ref from infra proj
    /// so, we create this abstraction to perform db operation in application layer using dbcontext instance throw DI
    /// </summary>
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; } 
        DbSet<OrderItem> OrderItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
