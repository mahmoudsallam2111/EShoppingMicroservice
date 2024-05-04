using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Orders.Exceptions;
using Ordering.Domain.StronglyTypedIds;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(request.OrderId);

            var orderToDelete = await dbContext.Orders
                .FindAsync([orderId] , cancellationToken);

            if (orderToDelete == null)
            {
                throw new OrderNotFoundException(request.OrderId);
            }
            dbContext.Orders.Remove(orderToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
