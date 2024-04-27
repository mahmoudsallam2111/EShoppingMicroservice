using BuildingBlocks.CQRS;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid orderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);