using BuildingBlocks.CQRS;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrdersByCustomerQueryHandler(IApplicationDbContext dbContext , IMapper mapper)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                 .AsNoTracking()
                 .Where(o => o.CustomerId.Value == request.CustomerId)
                  .OrderBy(o => o.OrderName.Value)    
                 .ToListAsync(cancellationToken);

             var ordersDtos = mapper.Map<List<OrderDto>>(orders);

            return new GetOrdersByCustomerResult(ordersDtos);
        }
    }
}
