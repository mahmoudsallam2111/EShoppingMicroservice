using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto)
    :ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

