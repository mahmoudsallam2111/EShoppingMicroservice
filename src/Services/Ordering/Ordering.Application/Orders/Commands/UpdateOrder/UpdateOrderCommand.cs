using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto OrderDto) :ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);
