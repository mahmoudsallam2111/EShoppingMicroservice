using BuildingBlocks.CQRS;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;
using System.Collections.Generic;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrderByNameQueryHandler(IApplicationDbContext dbContext)
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


            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
