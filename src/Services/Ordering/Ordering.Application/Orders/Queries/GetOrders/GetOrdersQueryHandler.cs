using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext dbContext , IMapper mapper)
        : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
    {
        public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var pageIndex = request.PaginationRequest.PageIndex;    
            var pageSize = request.PaginationRequest.PageSize;    

            var totalCountOfOrders = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                                .Include(o=>o.OrderItems)
                                .OrderBy(o=>o.OrderName.Value)
                                .Skip(pageIndex*pageSize)
                                .Take(pageSize)
                                .ToListAsync(cancellationToken);

            var ordersDto = mapper.Map<List<OrderDto>>(orders);

            return new GetOrdersQueryResult(new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCountOfOrders,
                ordersDto
                ));
            
        }
    }
}
