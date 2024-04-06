using Basket.Api.Basket.Queries.GetBasket;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.Commands.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSucceded);
    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapDelete("/basket/{userName}" , async (string userName , ISender sender ) =>
           {
               var result = await sender.Send(new DeleteBasketCommand(userName));
               var response = result.Adapt<DeleteBasketResponse>();

               return Results.Ok(response); 

           }).WithName("DeleteBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Delete Basket")
               .WithDescription("Delete Basket");
        }
    }
}
