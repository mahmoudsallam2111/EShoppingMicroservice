using Ordering.Domain.StronglyTypedIds;

namespace Ordering.Domain.Models;
/// <summary>
/// order is the aggregate root  && this is a rich domain model
/// </summary>
public class Order : Aggregate<OrderId>
{
    public CusomrerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }
    public static Order Create(OrderId id ,CusomrerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending,
        };

        order.RaiseDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        RaiseDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId , int quantity , decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var ordreItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(ordreItem); 
    }

    public void Remove(ProductId productId)
    {
        var ordreItem = _orderItems.FirstOrDefault(i=>i.ProductId == productId);

        if (ordreItem is {})
            _orderItems.Remove(ordreItem);
    }

}
