using Basket.Api.Basket.Queries.GetBasket;
using Basket.Api.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.Commands.CheckOutBasket;

public record CheckOutBasketRequest(BasketCheckOutDto BasketCheckOutDto);
public record CheckOutBasketResponse(bool IsSuccess);
public class CheckOutBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout" , async (CheckOutBasketRequest request , ISender sender) =>
        {
            var command = request.Adapt<CheckOutBasketCommand>();

            var result = await sender.Send(command);
            var response = result.Adapt<CheckOutBasketResponse>();

            return Results.Ok(response);
        }).WithName("CheckOutBasket")
                .Produces<CheckOutBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("CheckOutBasket")
               .WithDescription("Check Out Basket");
    }
}
