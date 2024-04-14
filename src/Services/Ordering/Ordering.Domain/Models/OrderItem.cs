namespace Ordering.Domain.Models;

/// <summary>
/// this entity does not have create method as it has no reasponsabilities 
/// the responsability of crate or remove order item relay on the order aggregate 
/// </summary>
public class OrderItem : Entity<OrderItemId>
{
    public OrderItem(OrderId orderId,ProductId productId , int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public OrderId OrderId { get;private set; }
    public ProductId ProductId { get; private set; }
    public int Quantity { get;private set; }
    public decimal Price { get;private set; }
}
