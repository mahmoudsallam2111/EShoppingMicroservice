using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    /// <summary>
    /// here we will raise an integration event to continue the process of fullfillment of the order
    /// </summary>
    /// <param name="publishEndpoint"></param>
    public class OrderCreatedEventHandler (IPublishEndpoint publishEndpoint , IFeatureManager featureManager , ILogger<OrderCreatedEventHandler> logger)
       : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handlled : {DomainEvent}",domainEvent.GetType().Name);


            // here we will use asp.featute management to enable only this feature(lines of code) 
            // when there is a subscriber to this , to prvent unneccessary messages
            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegarstionEvent = domainEvent.Order.ToOrderDto();
                await publishEndpoint.Publish(orderCreatedIntegarstionEvent, cancellationToken);
            }

        }
    }
}
