using Basket.Api.Data;
using Basket.Api.Dtos;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;

namespace Basket.Api.Basket.Commands.CheckOutBasket;

public record CheckOutBasketCommand(BasketCheckOutDto BasketCheckOutDto)
    : ICommand<CheckOutBasketResult>;

public record CheckOutBasketResult(bool IsSuccess);
public class CheckOutBasketCommandHandler(IBasketRepository basketRepository , IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckOutBasketCommand, CheckOutBasketResult>
{
    public async Task<CheckOutBasketResult> Handle(CheckOutBasketCommand request, CancellationToken cancellationToken)
    {
      var basket = await basketRepository.GetAsync(request.BasketCheckOutDto.UserName, cancellationToken);

        if (basket == null)
            return new CheckOutBasketResult(false);   // we will not raise an event

        var eventMessageToPublish = request.BasketCheckOutDto.Adapt<BasketCheckOutEvent>(); 

        eventMessageToPublish.TotalPrice = basket.TotalPrice;    


        // now we will publish the message to RMQ 

        await publishEndpoint.Publish(eventMessageToPublish , cancellationToken);

        // after publish a meaage we have to delete basket from basket db

        await basketRepository.DeleteAsync(request.BasketCheckOutDto.UserName , cancellationToken); 

        return new CheckOutBasketResult(true);


    }
}
