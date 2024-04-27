using BuildingBlocks.CQRS;
using MapsterMapper;
using Ordering.Application.Data;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler (IApplicationDbContext dbContext , IMapper mapper)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<Order>(request.OrderDto);  /// WE can do it explicitly

            dbContext.Orders.Add(order);    
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }
    }
}
