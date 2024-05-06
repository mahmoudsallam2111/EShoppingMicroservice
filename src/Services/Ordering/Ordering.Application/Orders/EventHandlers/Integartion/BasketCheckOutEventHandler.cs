using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integartion;

public class BasketCheckOutEventHandler(ILogger<BasketCheckOutEventHandler> logger , ISender sender)
      : IConsumer<BasketCheckOutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
    {
        logger.LogInformation("Intergarion Event Handled:{IntegartionEvent}", context);
        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckOutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName , message.EmailAddress , message.AddressLine , message.Country , message.State , message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardName, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId =Guid.NewGuid();

        var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems:
                [
                    new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                    new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
                ]);

        return new CreateOrderCommand(orderDto);
    }
}
