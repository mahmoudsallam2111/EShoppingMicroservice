using BuildingBlocks.CQRS;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrderByNameQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {

        async Task<GetOrdersByNameResult> IRequestHandler<GetOrdersByNameQuery, GetOrdersByNameResult>.Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
             .Include(o => o.OrderItems)
             .AsNoTracking()
             .Where(o => o.OrderName.Value == request.Name)
             .OrderBy(o => o.OrderName.Value)
             .ToListAsync(cancellationToken);

            var orderDtos = mapper.Map<List<OrderDto>>(orders);

            return new GetOrdersByNameResult(orderDtos);
        }
    }
}
