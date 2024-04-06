using Basket.Api.Models;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.Api.Basket.Queries.GetBasket
{
    public record GetBasketResponse(ShoppingCart ShoppingCart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}" , async (string UserName , ISender sender) =>
            {
                var result =await sender.Send(new GetBasketQuery(UserName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);

            }).WithName("GetBasketByUserName")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Get Basket By UserName")
               .WithDescription("Get Basket By UserName");
        }
    }
}
