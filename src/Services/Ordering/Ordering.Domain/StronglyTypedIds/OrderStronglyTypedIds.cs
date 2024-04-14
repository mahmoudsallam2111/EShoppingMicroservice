namespace Ordering.Domain.StronglyTypedIds;

public record CusomrerId
{
    public Guid Value { get;}
    private CusomrerId(Guid value) => Value = value; 

    public static CusomrerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("Customer Id can not be empty.");
        }

        return new CusomrerId(value);
    }
}

public record OrderName
{
    public OrderName(string value) => Value = value;

    public string Value { get; }

    private const int DefaultLength = 5;
    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(value));
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length , DefaultLength);

        return new OrderName(value);
    }
}

public record OrderId
{
    private OrderId(Guid value) => Value = value;
    public Guid Value { get; }
    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("order Id can not be empty.");
        }

        return new OrderId(value);
    }
}

public record OrderItemId
{
    private OrderItemId(Guid value) => Value = value;

    public Guid Value { get; }
    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("order Item Id can not be empty.");
        }

        return new OrderItemId(value);
    }
}


public record ProductId
{
    public ProductId(Guid value)=> Value = value;

    public Guid Value { get; }
    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("product Id can not be empty.");
        }

        return new ProductId(value);
    }
}