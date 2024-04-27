using BuildingBlocks.Exceptions;

namespace Ordering.Application.Orders.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid Id) : base("Order",Id)
        {
        }
    }
}
